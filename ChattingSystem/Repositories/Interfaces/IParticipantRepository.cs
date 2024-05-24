using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<Participant?>> Create(Participant participant);
        Task<Participant?> CreateObj(Participant participant);
        Task<Participant?> GetById(int? Id); 
        Task<IEnumerable<Participant?>> GetByConversationId(int? Id);
        Task<Participant?> GetByConversationIdObj(int? id);
        Task<Participant?> GetByConversationIdandUserId(int? userId, int? conversationId);
    }
}
