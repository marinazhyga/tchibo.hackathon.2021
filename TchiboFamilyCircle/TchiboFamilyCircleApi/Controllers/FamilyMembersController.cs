using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TchiboFamilyCircle.DomainService.Contracts;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyMembersController : ControllerBase
    {
        private readonly ILogger<FamilyMembersController> _logger;
        private IFamilyMemberService _familyMemberService;

        public FamilyMembersController(ILogger<FamilyMembersController> logger, IFamilyMemberService familyMemberService)
        {
            _logger = logger;
            _familyMemberService = familyMemberService;

            _logger.LogInformation("Test");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyMember>>> GetFamilyMembers()
        {
            _logger.LogInformation("GetFamilyMembers requested");
          
            try
            {
                var result = _familyMemberService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured", ex.Message);                
            }
           
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddFamilyMember([FromBody]FamilyMember familyMember)
        {
            _logger.LogInformation("AddFamilyMember requested");
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFamilyMember(int id, [FromBody] FamilyMember familyMember)
        {
            _logger.LogInformation("UpdateFamilyMember requested");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamilyMember(int id)
        {
            _logger.LogInformation("DeleteFamilyMember requested");
            return NoContent();
        }
    }    
}
