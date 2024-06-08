using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message>? Create(Message? message);
        Task<IEnumerable<Message>>? GetByConversationId(int? conversationId);
        Task<Message>? DeleteByConId(int? conId);
        Task<IEnumerable<Message>> DeleteByConversationIdAndParticipantId(int? groupId, int? userId);

    }
}
