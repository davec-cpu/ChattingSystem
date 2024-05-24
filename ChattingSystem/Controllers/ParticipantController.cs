using ChattingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChattingSystem.Controllers
{
    [ApiController]
    [Route("participant")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;
        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpGet("{userId}/{conId}")]
        public async Task<IActionResult> GetByUserId(int userId, int conId)
        {
           try
            {
                var result = await _participantService.GetByConversationAndUserId(conId, userId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest("Something went wrong");
            }
        }
    }
}
