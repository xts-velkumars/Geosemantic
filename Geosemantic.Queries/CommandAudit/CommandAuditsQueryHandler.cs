using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ste.Data;
using Ste.ViewModel;

namespace Ste.Queries.CommandAudit
{
    public class CommandAuditsQueryHandler : IRequestHandler<CommandAuditsQuery, IEnumerable<CommandAuditsViewModel>>
    {
        private readonly SteEntities context;

        public CommandAuditsQueryHandler(SteEntities context)
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
