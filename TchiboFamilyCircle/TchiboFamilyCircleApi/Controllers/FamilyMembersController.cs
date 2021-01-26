using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TchiboFamilyCircle.DomainService;
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
        }

        [HttpGet]
        public ActionResult<IEnumerable<FamilyMember>> GetFamilyMembers()
        {
            _logger.LogInformation("GetFamilyMembers requested");

            try
            {
                var result = _familyMemberService.GetAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured {@ex}", ex.Message);
                return BadRequest(ex.Message);
            }          
        }

        [HttpPost]
        public IActionResult AddFamilyMember([FromBody] FamilyMember familyMember)
        {
            _logger.LogInformation("AddFamilyMember requested {@familyMember}", familyMember);

            try
            {
                _familyMemberService.Add(familyMember);               
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured {@ex}", ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateFamilyMember([FromBody] FamilyMember familyMember)
        {
            _logger.LogInformation("UpdateFamilyMember requested {@familyMember}", familyMember);

            try
            {
                _familyMemberService.Update(familyMember);               
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured {@ex}", ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFamilyMember(string id)
        {
            _logger.LogInformation("DeleteFamilyMember requested {@id}", id);

            try
            {
                _familyMemberService.Delete(id);              
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured {@ex}", ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }    
}
