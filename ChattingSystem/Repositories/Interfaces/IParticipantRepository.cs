using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IParticipantRepository
    {
        Task<Participant>? Create(Participant participant);
        Task<Participant>? GetById(int? Id); 
        Task<IEnumerable<Participant?>> GetByConversationId(int? Id);
        Task<Participant>? GetByConversationIdObj(int? id);
        Task<Participant>? GetByConversationIdandUserId(int? userId, int? conversationId);
        Task<IEnumerable<Participant>>? DeleteByConId(int? conId);
        Task<Participant>? DeleteByConIdAndUserId(int? conId, int? userId);
        Task<int>? GetUserId(int? participantId);
    }
}
