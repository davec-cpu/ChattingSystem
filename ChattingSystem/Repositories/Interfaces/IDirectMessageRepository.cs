using ChattingSystem.Models;

namespace ChattingSystem.Repositories.Interfaces
{
    public interface IDirectMessageRepository
    {
        Task<DirectMessage> Create(DirectMessage message);
        Task<IEnumerable<DirectMessage?>> GetAllMsgsBySenderIdAndReceiverId(int senderId, int receiverId);
    }
}
