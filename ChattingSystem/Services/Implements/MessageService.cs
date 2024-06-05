using ChattingSystem.Models;
using ChattingSystem.Models.Expansions;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;
using Newtonsoft.Json;

namespace ChattingSystem.Services.Implements
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IParticipantRepository _participantRepository;
        private readonly IConversationRepository _conversationRepository;
        public MessageService(
            IMessageRepository messageRepository,
            IParticipantRepository participantRepository,
            IConversationRepository conversationRepository
            )
        {
            _messageRepository = messageRepository;
            _participantRepository = participantRepository;
            _conversationRepository = conversationRepository;
        }
        public async Task<Message?> Create(Message? message)
        {
            try
            {
                var result = await _messageRepository.Create(message);
                return result;
            }
            catch  
            {
                throw;
            }
        }

        public async Task<IEnumerable<Message>>? GetByConversationId(int? conversationId)
        {
            var result = await _messageRepository.GetByConversationId(conversationId);
            return result;
        }
        public async Task<IEnumerable<MessageExpansion.General?>> GetMsgExpByConversationId(int? conversationId)
        {
            var messages = await _messageRepository.GetByConversationId(conversationId);
            var result = new List<MessageExpansion.General>();
            foreach (var message in messages)
            {
                var participantId = message.ParticipantId;
                var conversationid = message.ConversationId;

                var participant = await _participantRepository.GetById(participantId);
                var conversation = await _conversationRepository.GetById(conversationid);

                var messageExp = new MessageExpansion.General(message)
                {
                    Conversation = conversation,
                    Participant = participant,
                };
                result.Add(messageExp);
            }
            return result;
        }
        public Task Push(IEnumerable<int>? userId, Message? message)
        {
 
                string result = $"{(message.Content)}";
                Console.WriteLine(result);
                return Task.CompletedTask;
        }
        public async Task<(IEnumerable<Message>?, int?)> GetByConversationIdWithTTRecords(int? conversationId)
        {
            var (data, totalRecords) = await _messageRepository.GetByConversationIdWithTTRecords(conversationId);
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
            return (data, totalRecords); 
        }

        public async Task<Message>? DeleteByConId(int? conversationId)
        {
            try
            {
                var result = await _messageRepository.DeleteByConId(conversationId);
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
