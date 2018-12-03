using System.Collections.Generic;
using Ste.ViewModel;
using Xen.Common.Interface;
using Xen.Query;

namespace Ste.Queries.User
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
