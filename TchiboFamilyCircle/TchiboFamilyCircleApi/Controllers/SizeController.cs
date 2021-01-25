using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SizeController : ControllerBase
    { 
        private readonly ILogger<SizeController> _logger;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("GetByMemberType")]
        public IEnumerable<string> GetSizesByMemberType(FamilyMemberType type, DateTime ? dateofBirth = null)
        {
            return new List<string>();
        }
    }
}
