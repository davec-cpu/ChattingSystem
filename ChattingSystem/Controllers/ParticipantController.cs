using ChattingSystem.Models;
using ChattingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChattingSystem.Controllers
{
    [ApiController]
    [Route("participant")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;
        private readonly IMessageService _messageService;
        private readonly IGroupUserService _groupUserService;
        private readonly IConversationGroupService _conversationGroupService;
        public ParticipantController(
            IParticipantService participantService,
            IMessageService messageService,
            IGroupUserService groupUserService,
            IConversationGroupService conversationGroupService
            )
        {
            _participantService = participantService;
            _messageService = messageService;
            _groupUserService = groupUserService;
            _conversationGroupService = conversationGroupService;
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

        [HttpPost]
        public async Task<IActionResult> Create(IEnumerable<Participant> participant)
        {
            try
            {
                var result = await _participantService.CreateMultiple(participant);
                return Ok("created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpDelete("delbycon/{conId}")]
        public async Task<IActionResult>? DeleteByConId(int? conId)
        {
            try
            {
                var result = await _participantService.DeleteByConId(conId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpDelete("removeaparticipant/{groupId}/{participantId}")]
        public async Task<IActionResult>? RemoveAParticipant(int? groupdId, int? participantId)
        {
            try
            {
                var conId = await _conversationGroupService.GetConversationIdByGroupId(groupdId);
                var userId = await _participantService.GetUserId(participantId);

                var groupUserResult = await _groupUserService.DeleteByGroupIdAndUserId(groupdId, userId);
                var messageResult = await _messageService.DeleteByConversationIdAndParticipantId(conId, participantId);
                var participantResult = await _participantService.DeleteByConIdAndUserId(conId, userId);

                return Ok("deleted");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("leaveagroup/{groupId}/{userId}")]
        public async Task<IActionResult>? LeaveAGroup(int? groupId, int? userId)
        {
            try
            {
                var conId = await _conversationGroupService.GetConversationIdByGroupId(groupId);
                var participantId = await _participantService.GetByConversationAndUserId(conId, userId);

                var groupUserResult = await _groupUserService.DeleteByGroupIdAndUserId(groupId, userId);
                var messageResult = await _messageService.DeleteByConversationIdAndParticipantId(conId, participantId.Id);
                var participantResult = await _participantService.DeleteByConIdAndUserId(conId, userId);

                return Ok("deleted");

            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
