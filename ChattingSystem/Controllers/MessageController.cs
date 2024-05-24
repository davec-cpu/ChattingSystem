﻿using ChattingSystem.Repositories.Interfaces;
using ChattingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChattingSystem.Controllers
{
    [ApiController]
    [Route("message")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IConversationGroupRepository _conversationGroupRepository;
        private readonly IParticipantRepository _participantRepository;
        public MessageController(
            IMessageService messageService
            , IParticipantRepository participantRepository
            , IConversationGroupRepository conversationGroupRepository)
        {
            _messageService = messageService;
            _participantRepository = participantRepository;
            _conversationGroupRepository = conversationGroupRepository;
        }

        [HttpGet("getbyconid/{conId}")]
        public async Task<IActionResult> GetByConversationId(int conId)
        {
            try
            {
                var result = await _messageService.GetByConversationId(conId);
                if (result == null)
                {
                    return NotFound("Record with required id doesnt exist");
                }
                return Ok(result);
            }
            catch
            {
                return NotFound("Something went wrong");
            }
        }
        [HttpGet("getmsgexpbyconid/{conId}")]
        public async Task<IActionResult> GetMsgExpByConversationId(int conId)
        {
            try
            {
                var result = await _messageService.GetMsgExpByConversationId(conId);
                if (result == null)
                {
                    return NotFound("Record with required id doesnt exist");
                }
                return Ok(result);
            }
            catch
            {
                return NotFound("Something went wrong");
            }
        }

        [HttpGet("getbygroupid/{groupId}")]
        public async Task<IActionResult> GetConversationIdByGroupId(int groupId)
        {
            try
            {
                var result = await _conversationGroupRepository.GetConversationIdByGroupId(groupId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet("getbyconidanduserid")]
        public async Task<IActionResult> GetConversationIdByGroupId()
        {
            try
            {
                var conversation = await _conversationGroupRepository.GetConversationIdByGroupId(2);
                Console.WriteLine("conversationid : " + conversation.ConversationId);
;                var result = await _participantRepository.GetByConversationIdandUserId(0, conversation.ConversationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
