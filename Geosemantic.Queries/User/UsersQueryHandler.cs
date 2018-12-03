using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geosemantic.Data;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Geosemantic.Queries.User
{
    public class UsersQueryHandler : IRequestHandler<UsersQuery, IEnumerable<UsersViewModel>>
    {
        private readonly SteEntities context;
        private readonly ILogger<UsersQueryHandler> logger;

        public UsersQueryHandler(SteEntities context, ILogger<UsersQueryHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<IEnumerable<UsersViewModel>> Handle(UsersQuery request, CancellationToken cancellationToken)
        {
            //var today = DateTime.Now;
            //bool isWeekEnd = today.IsWeekend();

            logger.LogInformation("Pulling user information");

            var users = await context.User
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return users.Select(Mapper.Map<UsersViewModel>);
        }
    }
}
