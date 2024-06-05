using ChattingSystem.Models;
using ChattingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChattingSystem.Controllers
{
    [Route("group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Group group)
        {
            try
            {
                var result = await _groupService.Create(group);
                if(result == null)
                {
                    return NotFound("not found");
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        [HttpDelete("{groupId}")]
        public async Task<IActionResult> Delete(int groupId)
        {
            try
            {
                var result = await _groupService.Delete(groupId);
                return Ok(result);

                //return Ok("group deleted");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
