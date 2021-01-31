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

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSizesByMemberType(FamilyMemberType ? type = null, DateTime ? birthDay = null)
        {
            _logger.LogInformation("GetSizesByMemberType requested");

            IEnumerable<string> result = new List<string>();

            try
            {
                if (type.HasValue)
                {
                    result = _sizeService.GetSizesByFamilyMember(type.Value, birthDay);
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
