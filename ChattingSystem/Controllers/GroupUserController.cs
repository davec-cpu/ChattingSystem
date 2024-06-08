using ChattingSystem.Models;
using ChattingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChattingSystem.Controllers
{
    [Route("groupuser")]
    [ApiController]
    public class GroupUserController : ControllerBase
    {
        private readonly IGroupUserService _groupUserService;
        public GroupUserController(IGroupUserService groupUserService)
        {
            _groupUserService = groupUserService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateMultiple(IEnumerable<GroupUser> groupUsers)
        {
            try
            {
                var result = await _groupUserService.CreateMultiple(groupUsers);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        [HttpDelete("delbycon/{groupId}")]
        public async Task<IActionResult> DeleteByGroupId(int groupId)
        {
            try
            {
                var result = await _groupUserService.DeleteByGroupId(groupId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
