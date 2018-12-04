using System.Threading;
using System.Threading.Tasks;
using Geosemantic.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xen.Common;

namespace Geosemantic.Command.User
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, XenResult>
    {
        private readonly GeosemanticEntities context;


        public DeleteUserCommandHandler(GeosemanticEntities context)
        {
            this.context = context;

        }

        public async Task<XenResult> Handle(DeleteUserCommand command, CancellationToken token)
        {
            var data = await context.User.SingleOrDefaultAsync(i => i.Id == command.Id, token);

            if (data == null)
                return new FailureResult("No record found");

            context.User.Remove(data);
            context.SaveChanges();
            return new SuccessResult();
        }
    }
}
