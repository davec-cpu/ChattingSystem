using ChattingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChattingSystem.Controllers
{
    [ApiController]
    [Route("directmsg")]
    public class DirectMessageController : ControllerBase
    {
        private readonly IDirectMessageService _directMessageService;

        public DirectMessageController(IDirectMessageService directMessageService)
        {
            _directMessageService = directMessageService;
        }

        [HttpGet("{senderId}/{receiverId}")]
        public async Task<IActionResult> GetBySenderIdnReceiverId(int senderId, int receiverId)
        {
            try
            {
                var result = await _directMessageService.GetAllMsgsBySenderIdAndReceiverId(senderId, receiverId);
                if(result == null)
                {
                    return NotFound("No record has been found");
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
