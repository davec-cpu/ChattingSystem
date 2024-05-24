using ChattingSystem.Models;

namespace ChattingSystem.Services.Interfaces
{
    public interface IGroupUserService
    {
        Task<GroupUser> Create(GroupUser groupUser);
        Task<IEnumerable<GroupUser>?> GetByUserId(int userId);
        Task<IEnumerable<GroupUser>?> GetByGroupId(int groupId);
    }
}
