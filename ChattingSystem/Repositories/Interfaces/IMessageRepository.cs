using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message?> Create(Message message);
        Task<IEnumerable<Message>> GetByConversationId(int? conversationId);
        Task<(IEnumerable<Message>?, int?)> GetByConversationIdWithTTRecords(int? conversationId);
    }
}
