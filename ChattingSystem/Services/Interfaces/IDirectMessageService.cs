using ChattingSystem.Models;
using ChattingSystem.Models.Expansions;

namespace ChattingSystem.Services.Interfaces
{
    public interface IDirectMessageService
    {
        Task<DirectMessage>? Create(DirectMessage? message);
        Task<IEnumerable<DirectMessageExpansion.General>>? GetAllMsgsBySenderIdAndReceiverId(int? senderId, int? receiverId);
    }
}
