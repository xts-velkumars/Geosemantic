using System.Collections.Generic;
using System.Threading.Tasks;
using Geosemantic.Command.User;
using Geosemantic.Queries.User;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xen.Api.Extensions;
using Xen.Common.Interface;

namespace Geosemantic.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator mediatr;
        private readonly ILoggedOnUserProvider user;
        private readonly ILogger<UserController> logger;

        public UserController(IMediator mediatr, ILoggedOnUserProvider user, ILogger<UserController> logger)
        {
            this.mediatr = mediatr;
            this.user = user;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("api/users")]
        public async Task<IEnumerable<UsersViewModel>> GetUsers()
        {
            logger.LogInformation("Executing  => api/users");
            return await mediatr.Send(new UsersQuery(user));
        }

        [HttpGet]
        [Authorize]
        [Route("api/user/{id}")]
        public async Task<UserViewModel> GetUser(long id)
        {
            return await mediatr.Send(new UserQuery(id));
        }


        [HttpPost]
        [Route("api/user")]
        public async Task<IActionResult> SaveUser([FromBody]SaveUserCommand command)
        {
            var result = await mediatr.Send(command);
            return result.ToActionResult();
        }

        [HttpPost]
        [Authorize]
        [Route("api/user/approved/{id}")]
        public async Task<IActionResult> ApprovedUser(long id)
        {
            var result = await mediatr.Send(new ApproveuserCommand(id, user));
            return result.ToActionResult();
        }

        [HttpDelete]
        [Authorize]
        [Route("api/user/{id}/delete")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await mediatr.Send(new DeleteUserCommand(id, user));
            return result.ToActionResult();
        }
    }
}