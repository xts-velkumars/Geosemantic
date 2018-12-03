using Ste.ViewModel;
using Xen.Query;

namespace Ste.Queries.User
{
    public class UserQuery : XenQuery<UserViewModel>
    {
        public long Id { get; set; }

        public UserQuery(long id)
        {
            Id = id;
        }
    }
}
