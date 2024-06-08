using ChattingSystem.Models;
using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Implements;
using ChattingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ChattingSystem.Controllers 
{
    
    [ApiController]
    [Route("user")]
    public class UserController:ControllerBase
    {
        private IUserService _userService;
        private readonly IParticipantRepository _participantRepository;
        private readonly IConversationRepository _conversationRepository;

        private readonly IParticipantService _participantService;
        private readonly IConversationService _conversationService;
        private readonly IMessageService _messageService;
        private readonly IGroupService _groupService;
        public UserController(IUserService userSerive, 
            IParticipantRepository participantRepository, 
            IConversationRepository conversationRepository, 
            IParticipantService participantService,
            IConversationService conversationService,
            IMessageService messageService,
            IGroupService groupService
            )
        {
            _userService = userSerive;
            _participantRepository = participantRepository;
            _conversationRepository = conversationRepository;
            _participantService = participantService;
            _conversationService = conversationService;
            _messageService = messageService;
            _groupService = groupService;
        }

        [HttpGet("/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _userService.GetById(id);
            if (emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Participant participant)
        {
            try
            {
                var result = await _participantService.Create(participant);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound("Something went wrong");
            }
        }
        

        [HttpGet("conversation/getbyuserId/{id}")]
        public async Task<IActionResult> GetpartByConversationId(int id)
        {
            try
            {
                var (result, record) = await _conversationService.GetByUserId(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

    
        [HttpPost("message/create")]
        public async Task<IActionResult> MsgCreate(Message message)
        {
            try
            {
                var result = await _messageService.Create(message);
                return Ok(result);
            }catch(Exception e)
            {
                return BadRequest("Something went wrong");
            }
        }

        //get all group by user
        [HttpGet("group/user/{userId}")]
        public async Task<IActionResult> GetGroupsByUserId(int userId)
        {
            try
            {
                var result = await _groupService.GetByUserId(userId);
                if(result == null)
                {
                    return NotFound("Not found");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet("getallexcept/{userId}")]
        public async Task<IActionResult> GetAllUsers(int userId)
        {
            try
            {
                var result = await _userService.GetAllUsersExceptOneSpecific(userId);
                if(result == null)
                {
                    return NotFound("NotFound");
                }
                return Ok(result);
            }catch(Exception ex)
            {
                throw;
            }
        }
        
    }
}
