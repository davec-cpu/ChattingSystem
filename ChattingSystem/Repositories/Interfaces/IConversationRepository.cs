using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        public Task<Conversation> GetById(int? Id);
        public Task<Conversation> Create(Conversation conversation);
        public Task<(IEnumerable<Conversation>, int?)> GetByUserId(int? userId);
    }
}
