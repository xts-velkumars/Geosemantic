using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Geosemantic.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xen.Common;
using Xen.Common.Constants;
using Xen.Common.Enums;
using Xen.Extensions;

namespace Geosemantic.Command.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, XenResult<IList<Claim>>>
    {
        private readonly GeosemanticEntities context;

        public LoginCommandHandler(GeosemanticEntities context)
        {
            this.context = context;
        }

        public async Task<XenResult<IList<Claim>>> Handle(LoginCommand cmd, CancellationToken token)
        {
            var userName = cmd.UserName;
            var hashedPassword = cmd.Password.ToPasswordHash();

         

            var loginResult = await context.User
                .AsNoTracking()
                .FirstOrDefaultAsync(i =>
                    string.Equals(i.EmailAddress, userName, StringComparison.CurrentCultureIgnoreCase)
                    && i.Password == hashedPassword
                    && i.StatusType == StatusType.Enabled, token);

            if (loginResult == null)
                return new FailureResult<IList<Claim>>("Invalid username or password");

            var claims = new List<Claim>
            {
                new Claim(ClaimConstants.UserId, loginResult.Id.ToString()),
                new Claim(ClaimConstants.FullName, loginResult.FullName)
            };

            return new SuccessResult<IList<Claim>>(claims);
            // Credentials are invalid, or account doesn't exist
        }
    }
}
