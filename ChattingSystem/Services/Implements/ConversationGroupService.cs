using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;

namespace ChattingSystem.Services.Implements
{
    public class ConversationGroupService : IConversationGroupService
    {
        private readonly IConversationGroupRepository _conversationGroupRepository;
        public ConversationGroupService(IConversationGroupRepository conversationGroupRepository)
        {
            _conversationGroupRepository = conversationGroupRepository;
        }
        public async Task<ConversationGroup>? Create(ConversationGroup? conversationGroup)
        {
            try
            {
                var result = await _conversationGroupRepository.Create(conversationGroup);
                return result;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<int>? GetConversationIdByGroupId(int? groupId)
        {
            try
            {
                var result = await _conversationGroupRepository.GetConversationIdByGroupId(groupId);
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
