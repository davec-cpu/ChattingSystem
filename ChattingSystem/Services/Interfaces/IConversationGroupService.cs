using ChattingSystem.Models;

namespace ChattingSystem.Services.Interfaces
{
    public interface IConversationGroupService
    {
        Task<ConversationGroup>? Create (ConversationGroup? conversationGroup);
        Task<int>? GetConversationIdByGroupId(int? groupId);
    }
}
