using Xen.Command;
using Xen.Common;
using Xen.Common.Interface;

namespace Geosemantic.Command.User
{
    public class DeleteUserCommand : XenCommand<XenResult>
    {
        public DeleteUserCommand(long id, ILoggedOnUserProvider user)
        {
            Id = id;
            SetUser(user);
        }

        public long Id { get; set; }
    }
}
