using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TchiboFamilyCircle.Dto;

namespace TchiboFamilyCircleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FamilyMemberController : ControllerBase
    {
        private readonly ILogger<FamilyMemberController> _logger;

        [HttpGet]
        public IEnumerable<FamilyMember> GetAll()
        {
            return new List<FamilyMember>();
        }

        [HttpPost]
        public bool Add([FromBody]FamilyMember familyMember)
        {
            return true;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            return true;
        }
    }    
}
