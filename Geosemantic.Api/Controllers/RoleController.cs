using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geosemantic.Queries.Role;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Geosemantic.Api.Controllers
{
    public class RoleController : Controller
    {
        private readonly IMediator mediatr;

        public RoleController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpGet]
        [Authorize]
        [Route("api/roleslookup")]
        public async Task<IEnumerable<LookUpViewModel>> GetRolesLookup()
        {
            return await mediatr.Send(new RolesLookupQuery());
        }

        [HttpGet]
        [Authorize]
        [Route("api/roles")]
        public async Task<IEnumerable<LookUpViewModel>> GetRoles()
        {
            return await mediatr.Send(new RolesQuery());
        }
    }
}
