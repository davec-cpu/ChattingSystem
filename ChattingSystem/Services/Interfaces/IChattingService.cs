using ChattingSystem.Models.Expansions;
using ChattingSystem.Repositories.Interfaces;

namespace ChattingSystem.Services.Interfaces
{
    public interface IChattingService
    {
        Task<MessageExpansion.General?> CreateMessage(MessageExpansion.General? general);
        Task<ConversationExpansion.General?> CreateConversation(ConversationExpansion.General? general);
        Task<(IEnumerable<ConversationExpansion.General?>, int?)> GetConversationsByUserId(int? userId);
        Task<(IEnumerable<MessageExpansion.General>?, int?)> GetMessagesByConversationId(int? conversationId);
    }
}
