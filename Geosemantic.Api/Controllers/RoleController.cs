using System.Collections.Generic;
using System.Threading.Tasks;
using Geosemantic.Queries.Role;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xen.Common.Interface;

namespace Geosemantic.Api.Controllers
{
    public class RoleController : Controller
    {
        private readonly IMediator mediatr;
        private readonly ILoggedOnUserProvider user;


        public RoleController(IMediator mediatr, ILoggedOnUserProvider user)
        {
            this.mediatr = mediatr;
            this.user = user;
        }

        [HttpGet]
        [Route("api/roleslookup")]
        public async Task<IEnumerable<LookUpViewModel>> GetRolesLookup()
        {
            return await mediatr.Send(new RolesLookupQuery());
        }

        [HttpGet]
        [Authorize]
        [Route("api/roles")]
        public async Task<IEnumerable<RolesViewModel>> GetRoles()
        {
            return await mediatr.Send(new RolesQuery(user));
        }
    }
}
