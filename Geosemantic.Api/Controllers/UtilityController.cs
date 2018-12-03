using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Geosemantic.Common;
using Geosemantic.Common.Enums;
using Geosemantic.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Xen.Extensions;

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


        [HttpGet]
        [Route("api/genders")]
        public IEnumerable<LookUpViewModel> GetGender()
        {
            var data = EnumExtensions.GetEnumByDescriptions(typeof(GenderType)).OrderBy(i => i.Value);
            return Mapper.Map<List<LookUpViewModel>>(data);
        }
    }
}
