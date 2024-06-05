using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IConversationGroupRepository
    {
        Task<ConversationGroup>? Create(ConversationGroup? conversationGroup);
        Task<int>? GetConversationIdByGroupId(int? groupId);
        Task<ConversationGroup>? Delete(int? groupId);
    }
}
