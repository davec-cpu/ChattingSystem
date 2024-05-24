using ChattingSystem.Models;

namespace ChattingSystem.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetByUserId (int? userId); 
    }
}
