using System.Collections.Generic;
using Geosemantic.ViewModel;
using Xen.Common.Interface;
using Xen.Query;

namespace Geosemantic.Queries.User
{
    public class UsersQuery : XenQuery<IEnumerable<UsersViewModel>>
    {
        public UsersQuery()
        {

        }

        public UsersQuery(ILoggedOnUserProvider user) : base(user)
        {
            SetUser(user);
        }
    }
}
