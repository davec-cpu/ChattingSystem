using ChattingSystem.Models;
using ChattingSystem.Models.Expansions;
using ChattingSystem.Repositories.Implements;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;

namespace ChattingSystem.Services.Implements
{
    public class DirectMessageService : IDirectMessageService
    {
        private readonly IDirectMessageRepository _directMessageRepository;
        private readonly IUserRepository _userRepository;
        public DirectMessageService(IDirectMessageRepository directMessageRepository, IUserRepository userRepository)
        {
            _directMessageRepository = directMessageRepository;
            _userRepository = userRepository;
        }
        public async Task<DirectMessage> Create(DirectMessage message)
        {
            try
            {
                var result = await _directMessageRepository.Create(message);
                return result;
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<IEnumerable<DirectMessageExpansion.General>> GetAllMsgsBySenderIdAndReceiverId(int senderId, int receiverId)
        {
            try
            {
                
                var messages = await _directMessageRepository.GetAllMsgsBySenderIdAndReceiverId(senderId, receiverId);
                var result = new List<DirectMessageExpansion.General>();
                Console.WriteLine("userNo1: " + senderId + "userNo2: " + receiverId);

                 
                
                foreach (var msg in messages)
                {
                    var sender = await _userRepository.GetById(msg.SenderId);
                    var receiver = await _userRepository.GetById(msg.ReceiverId);

                    var msgExpansion = new DirectMessageExpansion.General(msg)
                    {
                        Sender = sender,
                        Receiver = receiver,
                    };
                    result.Add(msgExpansion);
                }
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
