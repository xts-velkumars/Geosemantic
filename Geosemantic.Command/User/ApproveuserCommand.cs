using System;
using System.Collections.Generic;
using System.Text;
using Xen.Command;
using Xen.Common;
using Xen.Common.Interface;

namespace Geosemantic.Command.User
{
    public class ApproveuserCommand : XenCommand<XenResult>
    {
        public ApproveuserCommand(long id, ILoggedOnUserProvider user)
        {
            Id = id;
            SetUser(user);
        }

        public long Id { get; set; }
    }
}
