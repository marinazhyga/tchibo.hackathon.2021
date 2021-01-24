using Microsoft.Extensions.Logging;

namespace TchiboFamilyCircleApi.Controllers
{
    public class SizeController
    {
        private readonly ILogger<SizeController> _logger;

        private static readonly string[] Summaries = new[]
       {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
}
