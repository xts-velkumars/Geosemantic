using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geosemantic.Data;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Geosemantic.Queries.Page
{
    public class PagesQueryHandler : IRequestHandler<PagesQuery, IEnumerable<PagesViewModel>>
    {
        private readonly GeosemanticEntities context;
        public PagesQueryHandler(GeosemanticEntities context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PagesViewModel>> Handle(PagesQuery message, CancellationToken cancellationToken)
        {
            var pages = await context.Page
                .Where(x => x.RolePageMapping.Any(c => c.RoleId == message.RoleId))
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return pages.Select(Mapper.Map<PagesViewModel>);
        }
    }
}
