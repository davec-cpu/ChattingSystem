using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;

namespace ChattingSystem.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersExceptOneSpecific(int userId)
        {
            try
            {
                var result = await _userRepository.GetAllUsersExceptOneSpecific(userId);
                return result;
            }catch (Exception ex)
            {
                return null;
            }
        }

        public Task<User> GetById(int? Id)
        { 
            //if (Id == 0000) return null;
            var result = _userRepository.GetById(Id);
            return result;
        }
    }
}
