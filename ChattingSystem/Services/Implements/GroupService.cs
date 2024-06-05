using Azure.Core;
using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;
using Microsoft.OpenApi.Validations;
using Newtonsoft.Json;

namespace ChattingSystem.Services.Implements
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupUserRepository _groupUserRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IConversationGroupRepository _conversationGroupRepository;
        private readonly IMessageRepository _messageRepository;
        public GroupService(
            IGroupRepository groupRepository, 
            IGroupUserRepository groupUserRepository,
            IConversationRepository conversationRepository,
            IConversationGroupRepository conversationGroupRepository,
            IMessageRepository messageRepository
            )
        {
            _groupRepository = groupRepository;
            _groupUserRepository = groupUserRepository;
            _conversationRepository = conversationRepository;
            _conversationGroupRepository = conversationGroupRepository;
            _messageRepository = messageRepository;
        }

        public async Task<int?>? Create(Group? group)
        {
            try
            {
                //create group
                var groupResult = await _groupRepository.Create(group);

                Conversation con = new Conversation()
                {
                    SiteId = 1,
                    Title = "A group's title" 
                };
                //create con
                var conversationResult = await _conversationRepository.Create(con);
                ConversationGroup conversationGroup = new ConversationGroup()
                {
                    GroupId = groupResult.Id,
                    ConversationId = conversationResult.Id,
                };
                //create groupCon
                var groupCon = await _conversationGroupRepository.Create(conversationGroup);
                return conversationResult.Id;

            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public async Task<Group>? Delete(int? groupId)
        {
            try
            {
                var result = await _groupRepository.Delete(groupId);
                var groupConResult = await _conversationGroupRepository.Delete(groupId);

                var conId = await _conversationGroupRepository.GetConversationIdByGroupId(groupId);
                var conResult = await _conversationRepository.Delete(conId);
                var msgResult = await _messageRepository.DeleteByConId(conId);
               return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<IEnumerable<Group>>? GetByUserId(int? userId)
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
