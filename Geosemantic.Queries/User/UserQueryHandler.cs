using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geosemantic.Data;
using Geosemantic.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Geosemantic.Queries.User
{
    public class UserQueryHandler : IRequestHandler<UserQuery, UserViewModel>
    {
        private readonly SteEntities  context;
        public UserQueryHandler(SteEntities context)
        {
            this.context = context;
        }

        public async Task<UserViewModel> Handle(UserQuery message, CancellationToken cancellationToken)
        {
            var data = await context.User
                .SingleOrDefaultAsync(i => i.Id == message.Id, cancellationToken);

            var result = Mapper.Map<UserViewModel>(data);
            return result;
        }
    }
}
