using System.Collections.Generic;
using System.Threading.Tasks;
using Geosemantic.Queries.Page;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Geosemantic.Api.Controllers
{
    public class PageController : Controller
    {
        private readonly IMediator mediatr;
        public PageController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpGet]
        [Authorize]
        [Route("api/pages/{roleid}")]
        public async Task<IEnumerable<PagesViewModel>> GetPages(long roleId)
        {
            return await mediatr.Send(new PagesQuery(roleId));
        }
    }
}
