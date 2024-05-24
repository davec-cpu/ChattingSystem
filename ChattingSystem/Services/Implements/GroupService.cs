using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;
using Newtonsoft.Json;

namespace ChattingSystem.Services.Implements
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupUserRepository _groupUserRepository;
        public GroupService(IGroupRepository groupRepository, IGroupUserRepository groupUserRepository)
        {
            _groupRepository = groupRepository;
            _groupUserRepository = groupUserRepository;
        }
        public async Task<IEnumerable<Group>> GetByUserId(int? userId)
        {
            try
            {
                var groupUserList = await _groupUserRepository.GetByUserId(userId);
                var groupList = new List<Group>();
                foreach (var groupUser in groupUserList)
                {
                    var group = await _groupRepository.GetById(groupUser.GroupId);
                    groupList.Add(group);
                 }

                return groupList;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
