using ChattingSystem.Models;

namespace ChattingSystem.Services.Interfaces
{
    public interface IConversationService
    {
        Task<Conversation> GetById(int? Id);
        Task<Conversation?> Create(Conversation conversation);
        Task<(IEnumerable<Conversation>, int?)> GetByUserId(int? userId);
    }
}
