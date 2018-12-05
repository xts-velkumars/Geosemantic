using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geosemantic.Data;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xen.Common.Constants;

namespace Geosemantic.Queries.Role
{
    public class RolesLookupQueryHandlers : IRequestHandler<RolesLookupQuery, IEnumerable<LookUpViewModel>>
    {
        private readonly GeosemanticEntities context;

        public RolesLookupQueryHandlers(GeosemanticEntities context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<LookUpViewModel>> Handle(RolesLookupQuery message, CancellationToken cancellationToken)
        {
            message.DontAuditThisMessage();

            var data = await context.Role
                .AsNoTracking()
                .Where(i=> !i.Name.Equals(Common.Constants.RoleConstants.SuperAdmin))
                .ToListAsync(cancellationToken);
                
            return data.Select(Mapper.Map<LookUpViewModel>);
        }
    }
}
