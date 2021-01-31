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
    public class SizesController : ControllerBase
    { 
        private readonly ILogger<SizesController> _logger;
        private ISizeService _sizeService;

        public SizesController(ILogger<SizesController> logger, ISizeService sizeService)
        {
            _logger = logger;
            _sizeService = sizeService;
        }

        /// <summary>
        /// Get a list of sizes for a family member.
        /// </summary>
        /// <param name="familyMembertype"></param>
        /// <param name="birthDay"></param>
        /// <returns>empty</returns>
        /// <response code="200">Sizes have been received</response>
        /// <response code="400">Exception occurred during getting sizes</response> 
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSizesByMemberType(FamilyMemberType ? familyMembertype = null, DateTime ? birthDay = null)
        {
            _logger.LogInformation("GetSizesByMemberType requested");

            IEnumerable<string> result = new List<string>();

            try
            {
                if (familyMembertype.HasValue)
                {
                    result = _sizeService.GetSizesByFamilyMember(familyMembertype.Value, birthDay);
                }
                else 
                {
                    result = _sizeService.GetAllSizes();
                }               

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured {@ex}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
