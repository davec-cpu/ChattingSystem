using ChattingSystem.Models;
using ChattingSystem.Repositories.Implements;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;
using System.Text.RegularExpressions;

namespace ChattingSystem.Services.Implements
{
    public class GroupUserService : IGroupUserService
    {
        private readonly IGroupUserRepository _groupUserRepository;
        public GroupUserService(IGroupUserRepository groupUserRepository)
        {
            _groupUserRepository = groupUserRepository;
        }
        public async Task<GroupUser> Create(GroupUser groupUser)
        {
            try
            {
                var result = await _groupUserRepository.Create(groupUser);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<GroupUser>?> GetByGroupId(int groupId)
        {
            try
            {
                var result = await _groupUserRepository.GetByGroupId(groupId);
                return result;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<GroupUser>?> GetByUserId(int userId)
        {
            try
            {
                var result = await _groupUserRepository.GetByUserId(userId);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
