using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User>? GetById(int? Id);
        Task<IEnumerable<User>>? GetAllUsersExceptOneSpecific(int? userId);
    }
}
