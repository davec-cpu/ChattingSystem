using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;

namespace ChattingSystem.Services.Implements
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        public ConversationService(IConversationRepository conversationRepository)
        {
            _conversationRepository = conversationRepository;
        }
        public async Task<Conversation?> Create(Conversation conversation)
        {
            Conversation result = new Conversation();
            try
            {
                Console.WriteLine("Executing conversationService...");
                 result = await _conversationRepository.Create(conversation);
                return result;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
                return result;
            }
        }

        public async Task<Conversation> GetById(int? Id)
        {
            var result = await _conversationRepository.GetById(Id);
            return result;
        }

        public async Task<(IEnumerable<Conversation>, int?)> GetByUserId(int? userId)
        {
            var result = await _conversationRepository.GetByUserId(userId);
            return result;
        }

    }
}
