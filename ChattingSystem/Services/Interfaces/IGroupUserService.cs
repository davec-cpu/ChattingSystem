using ChattingSystem.Models;

namespace ChattingSystem.Services.Interfaces
{
    public interface IGroupUserService
    {
        Task<GroupUser>? Create(GroupUser? groupUser);
        Task<IEnumerable<GroupUser>>? GetByUserId(int? userId);
        Task<IEnumerable<GroupUser>>? GetByGroupId(int? groupId);
        Task<IEnumerable<GroupUser>>? CreateMultiple(IEnumerable<GroupUser>? groupUsers);
        Task<IEnumerable<GroupUser>>? DeleteByGroupId(int? groupId);
        Task<GroupUser>? DeleteByGroupIdAndUserId(int? groupId, int? userId);

    }
}
