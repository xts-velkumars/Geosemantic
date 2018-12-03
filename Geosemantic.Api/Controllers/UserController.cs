﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ste.Queries.User;
using Ste.ViewModel;
using Xen.Common.Interface;

namespace Ste.Api.Controllers
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

        //[HttpPost]
        //[Route("api/users")]
        //public async Task<IActionResult> SaveUser([FromBody]SaveUserCommand command)
        //{
        //    var result = await mediatr.Send(command);
        //    return result.ToActionResult();
        //}
    }
}