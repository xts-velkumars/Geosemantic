using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Geosemantic.Data;
using Geosemantic.ViewModel;
using MediatR;

namespace Geosemantic.Queries.CommandAudit
{
    public class CommandAuditsQueryHandler : IRequestHandler<CommandAuditsQuery, IEnumerable<CommandAuditsViewModel>>
    {
        private readonly GeosemanticEntities context;

        public CommandAuditsQueryHandler(GeosemanticEntities context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<CommandAuditsViewModel>> Handle(CommandAuditsQuery message, CancellationToken token)
        {
            //var data = await context.CommandAudit
            //    .AsNoTracking()
            //    .ToListAsync();

            //return data.Select(Mapper.Map<CommandAuditsViewModel>);
            return null;
        }
    }
}
