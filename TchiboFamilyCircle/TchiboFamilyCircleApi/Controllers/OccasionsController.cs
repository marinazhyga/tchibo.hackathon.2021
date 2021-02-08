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
    public class OccasionsController : ControllerBase
    {
        private readonly ILogger<OccasionsController> _logger;
        private IOccasionService _occasionService;

        public OccasionsController(ILogger<OccasionsController> logger, IOccasionService occasionService)
        {
            _logger = logger;
            _occasionService = occasionService;
        }

        /// <summary>
        /// Get a list of occasions.
        /// </summary>       
        /// <returns>a list of occasions</returns>
        /// <response code="200">List of occasions have been received</response>
        /// <response code="400">Exception occurred during getting occasions</response> 
        [HttpGet]
        public ActionResult<IEnumerable<Occasion>> GetOccasions()
        {
            _logger.LogInformation("GetOccasions requested");

            try
            {
                var result = _occasionService.GetAllOccasions();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured {@ex}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get an occasion by.
        /// </summary>       
        /// <returns>a occasion</returns>
        /// <response code="200">Occasion has been received</response>
        /// <response code="400">Exception occurred during getting occasion</response> 
        [HttpGet("{id}")]
        public ActionResult<Occasion> GetOccasionById(int id)
        {
            _logger.LogInformation("GetOccasions requested");

            try
            {
                var result = _occasionService.GetOccasionById(id);

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
