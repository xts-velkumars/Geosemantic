using Xen.Command;
using Xen.Common;

namespace Geosemantic.Command.User
{
    public class ApproveuserCommand : XenCommand<XenResult>
    {
        public long Id { get; set; }
    }
}
