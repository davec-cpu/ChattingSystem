using ChattingSystem.Models;

namespace ChattingSystem.Services.Interfaces
{
    public interface IConversationGroupService
    {
        Task<ConversationGroup> Create (ConversationGroup conversationGroup);
        Task<ConversationGroup> GetConversationIdByGroupId(int groupId);
    }
}
