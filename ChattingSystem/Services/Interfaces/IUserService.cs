using ChattingSystem.Models;

namespace ChattingSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetById(int? Id);
        Task<IEnumerable<User>> GetAllUsersExceptOneSpecific(int userId);
    }
}
