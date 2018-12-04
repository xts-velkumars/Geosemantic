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

            var data = await context.Role
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            data = data.OrderByDescending(i => i.CreationTs).ToList();
            data.ForEach(a => a.ModifyDatesToDisplay(message.User));
         
            return data.Select(Mapper.Map<RolesViewModel>);
        }
    }
}
