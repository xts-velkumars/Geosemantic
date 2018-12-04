using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Geosemantic.Common.Enums;
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
                .Include(i=>i.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(i =>
                    string.Equals(i.EmailAddress, userName, StringComparison.CurrentCultureIgnoreCase)
                    && i.Password == hashedPassword
                    && i.StatusType == StatusType.Enabled, token);

            if (loginResult == null)
                return new FailureResult<IList<Claim>>("Invalid username or password");

            var agentStatus = string.Empty;

            if (loginResult.RoleName ==  Common.Constants.RoleConstants.User)
                agentStatus = IsUserApproved(loginResult);


            var claims = new List<Claim>
            {
                new Claim(ClaimConstants.UserId, loginResult.Id.ToString()),
                new Claim(ClaimConstants.RoleId, loginResult.RoleId.ToString()),
                new Claim(ClaimConstants.RoleName, loginResult.RoleName),
                new Claim(ClaimConstants.FullName, loginResult.FullName),
                new Claim(ClaimConstants.Referrence1, agentStatus),
            };

            return new SuccessResult<IList<Claim>>(claims);
            // Credentials are invalid, or account doesn't exist
        }

        private static string IsUserApproved(Domain.Entities.User user)
        {
            var agentStatus = UserStatusType.WaitingForApproval.ToString();

           
            switch (user.UserStatusType)
            {
                case UserStatusType.WaitingForApproval:
                    agentStatus = UserStatusType.WaitingForApproval.ToString();
                    break;
                case UserStatusType.Approved:
                    agentStatus = UserStatusType.Approved.ToString();
                    break;
                case UserStatusType.Rejected:
                    agentStatus = UserStatusType.Rejected.ToString();
                    break;
            }
            return agentStatus;
        }
    }
}
