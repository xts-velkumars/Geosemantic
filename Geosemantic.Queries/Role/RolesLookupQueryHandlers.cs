using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geosemantic.Data;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                .ToListAsync(cancellationToken);

            return data.Select(Mapper.Map<LookUpViewModel>);
        }
    }
}
