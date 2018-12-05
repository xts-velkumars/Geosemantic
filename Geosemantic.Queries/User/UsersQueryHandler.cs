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
        private readonly GeosemanticEntities context;
        private readonly ILogger<UsersQueryHandler> logger;

        public UsersQueryHandler(GeosemanticEntities context, ILogger<UsersQueryHandler> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<IEnumerable<UsersViewModel>> Handle(UsersQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Pulling user information");

            var users = await context.User
                .Include(i=>i.Role)
                .AsNoTracking()
                .OrderByDescending(i=>i.LastChangeTs)
                .ThenBy(i=>i.UserStatusType)
                .ToListAsync(cancellationToken);

            return users.Select(Mapper.Map<UsersViewModel>);
        }
    }
}
