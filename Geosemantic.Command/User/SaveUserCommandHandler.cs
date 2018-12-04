using System.Threading;
using System.Threading.Tasks;
using Geosemantic.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xen.Common;
using AutoMapper;
using Geosemantic.Common.Enums;
using Xen.Entity.Extensions;

namespace Geosemantic.Command.User
{
    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, XenResult<bool>>
    {
        private readonly GeosemanticEntities context;

        public SaveUserCommandHandler(GeosemanticEntities context)
        {
            this.context = context;
        }

        public async Task<XenResult<bool>> Handle(SaveUserCommand command, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.User();
            if (command.Id > 0)
            {
                entity = await context.User.SingleOrDefaultAsync(i => i.Id == command.Id, cancellationToken);

                if (entity == null)
                    return new FailureResult<bool>("Could not find user information");
            }
            Mapper.Map(command, entity);

            entity.PopulateMetaData(command.LoggedOnUserId.ToString());

            if (command.Id == 0)
            {
                entity.UserSystemType = UserSystemType.UserDefined;
                context.User.Add(entity);
            }

            await context.SaveChangesAsync(cancellationToken);
            return new SuccessResult<bool>(true);
        }
    }
}
