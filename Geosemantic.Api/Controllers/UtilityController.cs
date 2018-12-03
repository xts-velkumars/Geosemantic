using System.Reflection;
using Geosemantic.Common;
using Microsoft.AspNetCore.Mvc;

namespace Geosemantic.Api.Controllers
{
    public class UtilityController : Controller
    {
        private readonly AppSettings settings;

        public UtilityController(AppSettings settings)
        {
            this.settings = settings;
        }

        [HttpGet]
        [Route("api/version")]
        public string GetVersion()
        {
            return $"{settings.Environment} {Assembly.GetExecutingAssembly().GetName().Version}";
        }
    }
}
