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
    public class FamilyCirclesController : ControllerBase
    {
        private readonly ILogger<FamilyCirclesController> _logger;
        private IFamilyMemberService _familyMemberService;
        private IFamilyCircleService _familyCircleService;

        public FamilyCirclesController(ILogger<FamilyCirclesController> logger, IFamilyMemberService familyMemberService, IFamilyCircleService familyCircleService)
        {
            _logger = logger;
            _familyMemberService = familyMemberService;
            _familyCircleService = familyCircleService;
        }

        [HttpGet]
        public ActionResult<List<Article>> GetArticlesPerFamilyMember([FromQuery] string familyMemberId = "601fff2a5b7e4d16d9d14f60", [FromQuery] int occasionId = 1)
        {
            _logger.LogInformation("GetArticlesByFamilyMemberId requested");

            try
            {
                var result = _familyMemberService.GetById(familyMemberId);
                                
                var articles = _familyCircleService.GetArticlesPerFamilyMember(result, occasionId);

                return Ok(articles);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured {@ex}", ex.Message);
                return BadRequest(ex.Message);
            }            
        }
    }
}
