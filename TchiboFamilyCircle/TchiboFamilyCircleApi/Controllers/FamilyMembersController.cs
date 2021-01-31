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

        /// <summary>
        /// Get a list of FamilyMembers.
        /// </summary>
        /// <param name="familyMember"></param>
        /// <returns>empty</returns>
        /// <response code="200">FamilyMembers have been created received</response>
        /// <response code="400">Exception occurred during getting FamilyMembers</response> 
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

        /// <summary>
        /// Creates a new FamilyMember.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///          "name": "Ursula",
        ///          "type": "Mother",
        ///          "dateOfBirth": "1962-12-26",
        ///          "occasions": [
        ///              {
        ///               "id": 1,
        ///               "name": "Birthday",
        ///               "date": "2021-09-24"
        ///               },
        ///               {
        ///               "id": 2,
        ///               "name": "Christmas",
        ///               "date": "2021-12-25"
        ///               },
        ///               {
        ///               "id": 3,
        ///               "name": "Mother'sDay",
        ///               "date": "2021-05-09"
        ///               }
        ///          ],
        ///          "sizes": "40/42, M 40/42",
        ///          "interests": "music, sport, jewerly, swimming, backing, traveling",
        ///          "customerNumber": "7584947365"
        ///          }
        ///
        /// </remarks>
        /// <param name="familyMember"></param>
        /// <returns>empty</returns>
        /// <response code="200">FamilyMember has been created successfully</response>
        /// <response code="400">Exception occurred during FamilyMember creation</response>        
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

        /// <summary>
        /// Updates an existing FamilyMember.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///          "id": "6010a9b11242ed7b85d89d7f",
        ///          "name": "Ursula",
        ///          "type": "Grandma",
        ///          "dateOfBirth": "1942-09-24",
        ///          "occasions": [
        ///              {
        ///               "id": 1,
        ///               "name": "Birthday",
        ///               "date": "2021-12-26"
        ///               },
        ///               {
        ///               "id": 2,
        ///               "name": "Christmas",
        ///               "date": "2021-12-25"
        ///               }
        ///          ],
        ///          "sizes": "40/42, M 40/42",
        ///          "interests": "music, jewerly, backing, traveling",
        ///          "customerNumber": "7584947365"
        ///          }
        ///
        /// </remarks>
        /// <param name="familyMember"></param>
        /// <returns>empty</returns>
        /// <response code="200">FamilyMember has been created successfully</response>
        /// <response code="400">Exception occurred during FamilyMember creation</response>  
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

        /// <summary>
        /// Deletes a FamilyMember.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     {
        ///          "id": "6010a9b11242ed7b85d89d7f"       
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Unique id of FamilyMember</param>
        /// <returns>empty</returns>
        /// <response code="200">FamilyMember has been deleted successfully</response>
        /// <response code="400">Exception occurred during FamilyMember deletion</response>    
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
