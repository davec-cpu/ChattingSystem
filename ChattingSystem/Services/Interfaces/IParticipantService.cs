using ChattingSystem.Models;

namespace ChattingSystem.Services.Interfaces
{
    public interface IParticipantService
    {
        Task<IEnumerable<Participant>>? Create(Participant? participant);
        Task<Participant>? GetById(int? Id);
        Task<IEnumerable<Participant>>? GetByConversationId(int? Id);
        Task<Participant>? GetByConversationIdObj(int? Id);
        Task<Participant>? CreateObj(Participant? participant);
        Task<Participant>? GetByConversationAndUserId(int? ConversationId, int? UserId);
        Task<IEnumerable<Participant>>? CreateMultiple(IEnumerable<Participant>? participant);
        Task<IEnumerable<Participant>>? DeleteByConId(int? conId);
    }
}
