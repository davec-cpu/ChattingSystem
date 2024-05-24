using ChattingSystem.Models;
using ChattingSystem.Models.Expansions;

namespace ChattingSystem.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> Create(Message message);
        Task Push(IEnumerable<int?> userId, Message message);
        Task<IEnumerable<Message?>> GetByConversationId(int? conversationId);
        Task<IEnumerable<MessageExpansion.General?>> GetMsgExpByConversationId(int? conversationId);
        Task<(IEnumerable<Message>?, int?)> GetByConversationIdWithTTRecords(int? conversationId);
    }
}
