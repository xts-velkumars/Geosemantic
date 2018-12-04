using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geosemantic.Common.Enums;
using Geosemantic.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xen.Common;

namespace Geosemantic.Command.User
{
   public class ApproveuserCommandHandler : IRequestHandler<ApproveuserCommand, XenResult>
    {
        private readonly IMediator mediatr;
        private readonly GeosemanticEntities context;

        public ApproveuserCommandHandler(IMediator mediatr, GeosemanticEntities context)
        {
            this.mediatr = mediatr;
            this.context = context;
        }

        public async Task<XenResult> Handle(ApproveuserCommand command, CancellationToken token)
        {
            var data = await context.User.SingleOrDefaultAsync(i => i.Id == command.Id, cancellationToken: token);

            if (data == null)
                return new FailureResult("No record found");

            data.UserStatusType = UserStatusType.Approved;
            await context.SaveChangesAsync(token);

            //Send email for accepeted agent
            return await mediatr.Send(new SendUserConfirmationEmailCommand(command)
            {
              UserName = data.FullName,
              UserEmail = data.EmailAddress

            }, token);
        }
    }
}
