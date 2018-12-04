using System.Collections.Generic;
using Geosemantic.ViewModel;
using Xen.Common.Interface;
using Xen.Query;

namespace Geosemantic.Queries.Role
{
    public class RolesQuery : XenQuery<IEnumerable<RolesViewModel>>
    {
        public RolesQuery()
        {

        }

        public RolesQuery(ILoggedOnUserProvider user) : base(user)
        {
            SetUser(user);
        }
    }
}
