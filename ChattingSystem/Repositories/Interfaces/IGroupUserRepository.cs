using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IGroupUserRepository
    {
        Task<GroupUser?> Create(GroupUser groupUser);
        Task<IEnumerable<GroupUser>>? GetByUserId(int? userId);
        Task<IEnumerable<GroupUser>>? GetByGroupId(int? groupId);
        Task<IEnumerable<GroupUser>>? DeleteByGroupId(int? groupId);
    }
}
