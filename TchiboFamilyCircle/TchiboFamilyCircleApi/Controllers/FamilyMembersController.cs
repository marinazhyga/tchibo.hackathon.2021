using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyMembersController : ControllerBase
    {
        private readonly ILogger<FamilyMembersController> _logger;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyMember>>> GetFamilyMembers()
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddFamilyMember([FromBody]FamilyMember familyMember)
        {
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AddFamilyMember(int id, [FromBody] FamilyMember familyMember)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamilyMember(int id)
        {
            return NoContent();
        }
    }    
}
