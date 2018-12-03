using Geosemantic.ViewModel;
using Xen.Query;

namespace Geosemantic.Queries.User
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
