using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetById(int? groupId);
    }
}
