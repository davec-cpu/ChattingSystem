using ChattingSystem.DataService;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient.DataClassification;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace ChattingSystem.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IConversationGroupRepository _conversationGroupRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly TempDb _tempDb;
        public ChatHub(
                IConversationGroupRepository conversationGroupRepository,
                IParticipantRepository participantRepository,
                IMessageRepository messageRepository,
                IUserRepository userRepository,
                IDirectMessageRepository directMessageRepository,
                TempDb tempDb
                
        )
        {
            _conversationGroupRepository = conversationGroupRepository;
            _participantRepository = participantRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _tempDb = tempDb;
            _directMessageRepository = directMessageRepository;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("connected", "connection has been created");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("Disconnected", "connection disconnected");
            var userId = _tempDb.connectedUserId.First(x => x.Value == Context.ConnectionId).Key;
            string value;
            _tempDb.connectedUserId.TryRemove(userId, out value); 
            var userList = new List<string>(_tempDb.connectedUserId.Values);
            var listcvt = JsonConvert.SerializeObject(userList, Formatting.Indented);
            await Clients.All.SendAsync("UserDisconnected", listcvt);
            await base.OnDisconnectedAsync(ex);
        }
 
 
        public async Task JoinAChatRoom(int groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "group no." + groupId.ToString());
            _tempDb.connection[Context.ConnectionId] = groupId.ToString();
            await Clients.All.SendAsync("JoinAChatRoom", "admin", "An user has joined");
        }

        public async Task SendMessage(int userId, int groupId, string message)
        {
            try
            {
                var conversationId = await _conversationGroupRepository.GetConversationIdByGroupId(groupId);
                var participant = await _participantRepository.GetByConversationIdandUserId(conversationId, userId);
                var messageTempId = 0;
                if (_tempDb.userMessage.Any())
                {
                    var lastItem = _tempDb.userMessage.Last();
                    messageTempId = lastItem.Key + 1;
                    _tempDb.userMessage[messageTempId] = userId;
                }else
                {
                    _tempDb.userMessage[0] = userId;
                }

                var messageCreate = new Message
                    {
                        ConversationId = conversationId,
                        Content = message,
                        ParticipantId = participant.Id,
                        Status = 1,
                        SiteId = 1
                    };  
                    var result = await _messageRepository.Create(messageCreate);

                    await Clients
                        .Group("group no." + groupId.ToString())
                        .SendAsync("SendMessage", participant.Title, message, messageTempId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        
 
        public async Task Login(int userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "user no." + userId.ToString());

            _tempDb.connectedUserId[userId.ToString()] = Context.ConnectionId;
            var userList = new List<string>(_tempDb.connectedUserId.Keys);

            var listcvt =  JsonConvert.SerializeObject(userList, Formatting.Indented);
   
            
            await Clients.All.SendAsync("Login", Context.ConnectionId, listcvt);
        }

        public async Task SendDM(int senderId, int receiverid, string msg)
        {
            try
            {
                var DMCreate = new DirectMessage
                {
                    SenderId = senderId,
                    ReceiverId = receiverid,
                    Content = msg,
                    CreatedAt = DateTime.Now.ToString("h:mm:ss")
                };
                var result = await _directMessageRepository.Create(DMCreate);
                var sender = await _userRepository.GetById(senderId);
                await Groups.AddToGroupAsync(Context.ConnectionId, "user no." + receiverid.ToString());

                await Clients
                    .Group("user no." + receiverid.ToString())
                    .SendAsync("SendDM", msg, senderId, sender.Name);
            }

            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task CreateGroup(int[] idArr, int groupId, string groupTitle)
        {
            try
            {

                foreach (var i in idArr)
                {
                    if (_tempDb.connectedUserId.TryGetValue(i.ToString(), out string connectionId))
                    {
                        await Groups.AddToGroupAsync(connectionId, "newcreatedgroup." + groupId.ToString());
                    }
                }

                string msg = "You have been added to group " + groupTitle;

                await Clients
                       .Group("newcreatedgroup." + groupId.ToString())
                       .SendAsync("CreateGroup", msg);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
