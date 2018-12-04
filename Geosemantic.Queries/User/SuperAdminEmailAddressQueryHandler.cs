using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Geosemantic.Common.Constants;
using Geosemantic.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xen.Extensions;

namespace Geosemantic.Queries.User
{
    public class SuperAdminEmailAddressQueryHandler : IRequestHandler<SuperAdminEmailAddressQuery, string>
    {
        private readonly GeosemanticEntities context;

        public SuperAdminEmailAddressQueryHandler(GeosemanticEntities context)
        {
            this.context = context;
        }

        public async Task<string> Handle(SuperAdminEmailAddressQuery message, CancellationToken token)
        {
            var adminId = await context.Role
                .AsNoTracking()
                .FirstAsync(i => i.Name.Equals(RoleConstants.SuperAdmin, StringComparison.CurrentCultureIgnoreCase),  token);

            var data = await context.User
                .AsNoTracking()
                .Where(i => i.RoleId == adminId.Id)
                .ToListAsync(token);

            var adminEmailList = data.Select(i => i.EmailAddress);

            return adminEmailList.JoinWithComma();
        }
    }
}
 
