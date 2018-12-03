using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Ste.Common;

namespace Ste.Api.Controllers
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
