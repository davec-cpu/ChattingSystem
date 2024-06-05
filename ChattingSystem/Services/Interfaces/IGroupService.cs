using ChattingSystem.Models;

namespace ChattingSystem.Services.Interfaces
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>>? GetByUserId (int? userId);
        Task<int?>? Create(Group? group);
        Task<Group?>? Delete(int? groupId);
    }
}
