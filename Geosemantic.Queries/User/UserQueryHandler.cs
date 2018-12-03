using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ste.Data;
using Ste.ViewModel;

namespace Ste.Queries.User
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
