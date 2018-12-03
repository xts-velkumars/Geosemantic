using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geosemantic.Data;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xen.Query;

namespace Geosemantic.Queries.Role
{
    public class RolesQueryHandlers : IRequestHandler<RolesQuery, IEnumerable<RolesViewModel>>
    {
        private readonly GeosemanticEntities context;

        public RolesQueryHandlers(GeosemanticEntities context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<RolesViewModel>> Handle(RolesQuery message, CancellationToken cancellationToken)
        {
            message.DontAuditThisMessage();

            var roles = await context.Role
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return roles.Select(Mapper.Map<RolesViewModel>);
        }
    }
}
