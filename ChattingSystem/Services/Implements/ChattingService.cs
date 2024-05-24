using ChattingSystem.Models;
using ChattingSystem.Models.Expansions;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;
using System.Data.Common;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Transactions;

namespace ChattingSystem.Services.Implements
{
    public class ChattingService : IChattingService
    {
        private readonly IUserService _userService;
        private readonly IParticipantService _participantService;
        private readonly IConversationService _conversationService;
        private readonly IMessageService _messageService;
        private readonly IConversationGroupService _conversationGroupService;

        public ChattingService(IUserService userService, IParticipantService participantService, IConversationService conversationService, IMessageService messageService, IConversationGroupService conversationGroupService)
        {
            _userService = userService;
            _participantService = participantService;
            _conversationService = conversationService;
            _messageService = messageService;
            _conversationGroupService = conversationGroupService;
        }

        public async Task<MessageExpansion.General?> CreateMessage(MessageExpansion.General? general)
        {
            try
            {
                if (general == null)
                {
                    Console.WriteLine("general null");
                    return null;
                }
                if (general.ParticipantId == null)
                {
                    Console.WriteLine("participantId null");
                    return null;
                }
                if (general.ConversationId == null)
                {
                    Console.WriteLine("ConversationId null");
                    return null;
                }

                var participant = await _participantService.GetById(general.ParticipantId);
                if (participant == null)
                {
                    Console.WriteLine("participant null");
                    return null;
                }

                var conversation = await _conversationService.GetById(general.ConversationId);
                if (conversation == null)
                {
                    Console.WriteLine("conversation null");
                    return null;
                }

                var user = await _userService.GetById(participant.UserId);
                if (user == null)
                {
                    Console.WriteLine("user null");
                    return null;
                }

                var message =  await _messageService.Create(Message.From(general));

                if (message == null)
                {
                    Console.WriteLine("message null");
                    return null;
                }

                var participants = await _participantService.GetByConversationId(message.ConversationId);
                //Nếu như có thành viên với id cuộc trò chuyện, đẩy tin nhắn tới tất cả thành viên đó
                if (participants.Count() > 0)
                {
                    Console.WriteLine("participants greater than 0");
                    //Lấy danh sách thành viên
                    var userId = participants.Select(p => p.UserId);
                    //đẩy tin nhắn
                    await _messageService.Push(userId, message);
                }
                //Trả về phản hồi với thông tin tin nhắn, người gửi, người dùng
                return new MessageExpansion.General(message)
                {
                    User = user,
                    Participant = participant,
                    Conversation = conversation,
                };
            }catch (Exception e)
            {
                Console.WriteLine("An exception has been thrown");
                throw;
            }
        }

        public async Task<ConversationExpansion.General?> CreateConversation (ConversationExpansion.General? general)
        {
            try
            {
                Console.WriteLine("trying to creaate conversation...  ");
                if (general == null)
                {
                    Console.WriteLine("general null");
                    return null;
                }
                //Lấy ra đoạn hội thoại
                var conversation = Conversation.From(general);
                conversation = await _conversationService.Create(conversation);
                string conjson = JsonConvert.SerializeObject(conversation, Formatting.Indented);
                Console.WriteLine("conversation result:");
                //Console.WriteLine(conjson);

                if (conversation == null)
                {
                    Console.WriteLine("conversation null");
                    return null;
                }
                //Neu nhu co group, tao moi conversation-group tuong ung voi moi group id
                if (general.Groups != null)
                {
                    foreach (var group in general.Groups)
                    {
                        if (group.Id == null) continue;
                        var createdcon =  await _conversationGroupService.Create(new ConversationGroup
                        {
                            SiteId = conversation.SiteId,
                            ConversationId = conversation.Id,
                            GroupId = group.Id
                        });
                        //Console.WriteLine($"con_group: {createdcon.ConversationId}");
                        //Console.WriteLine(JsonConvert.SerializeObject(createdcon, Formatting.Indented));
                    }                    
                }
                //Tao danh sach nguoi tham gia tu general
                var participants = new List<Participant>();
                if(general.Participants != null)
                {
                    foreach(var participant in general.Participants)
                    {
                        var newParticipant = await _participantService.CreateObj(new Participant
                        {
                            SiteId = conversation.SiteId,
                            UserId = participant.Id,
                            ConversationId = conversation.Id,
                            Title = participant.Title,
                            Type = participant.Type,
                            Taxonomy = participant.Taxonomy,
                            Settings = participant.Settings,
                            Status = participant.Status,
                        });

                        if (newParticipant != null) participants.Add(newParticipant);
                    }
                    var userids = participants.Select(p => p.UserId);
                }

                return new ConversationExpansion.General(conversation)
                {
                    Groups = general.Groups,
                    Participants = general.Participants,
                };
            }
            catch (Exception ex) 
            {
                Console.WriteLine("An exception has been thrown");
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<(IEnumerable<ConversationExpansion.General?>, int?)> GetConversationsByUserId (int? userId)
        {
            var result = ((Enumerable.Empty<ConversationExpansion.General>(), default(Nullable<int>)));
            try
            {
                var (data, totalRecord) = await _conversationService.GetByUserId(userId);
                var conversations = data?.ToList();

                if (conversations != null && conversations.Count != 0)
                {
                    var generalConversations = new List<ConversationExpansion.General>();
                    foreach (var conversation in conversations)
                    {
                        var general = new ConversationExpansion.General(conversation)
                        {
                            Participants = await this.GetParticipantsByConversationId(conversation.Id)
                        };
                        generalConversations.Add(general);
                    }
                    result = (generalConversations, totalRecord ?? 0);
                }
            }
            catch
            {
                throw;
            }
            
            return result;
        }

        public async Task<(IEnumerable<MessageExpansion.General>?, int?)> GetMessagesByConversationId (int? conversationId)
        {
            var result = (Enumerable.Empty<MessageExpansion.General>(), default(Nullable<int>));
            try
            {
                if (conversationId == null) return result;
                var (data, totalRecord) = await _messageService.GetByConversationIdWithTTRecords(conversationId);
                var messages = data.ToList();
                if(messages != null && messages.Count != 0)
                {
                    var generalMessages = new List<MessageExpansion.General>();
                    foreach(var message in messages)
                    {
                        var general = new MessageExpansion.General(message)
                        {
                            Participant = await _participantService.GetById(message.ParticipantId),
                            Conversation = await _conversationService.GetById(message.ConversationId),
                        };
                        if(general.Participant != null)
                        {
                            general.User = await _userService.GetById(general.Participant.UserId);
                        }
                        generalMessages.Add(general);
                    }
                    Console.WriteLine(JsonConvert.SerializeObject(generalMessages, Formatting.Indented));
                    result = (generalMessages, totalRecord);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        public async Task<IEnumerable<ParticipantExpansion.General>> GetParticipantsByConversationId (int? conversationId)
        {
            try
            {
                var participants = await _participantService.GetByConversationId(conversationId);
                var uuu = new List<ParticipantExpansion.General>();

                foreach(var participant in participants)
                {
                    var xxx = new ParticipantExpansion.General(participant);
                    xxx.Avatar = "";
                    uuu.Add(xxx);
                }
                return uuu;
            }
            catch
            {
                throw;
            }
            return null;
        }
    }
}
