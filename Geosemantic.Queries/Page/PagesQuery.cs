using System.Collections.Generic;
using Geosemantic.ViewModel;
using Xen.Query;

namespace Geosemantic.Queries.Page
{
    public class PagesQuery : XenQuery<IEnumerable<PagesViewModel>>
    {
        public long RoleId { get; set; }

        public PagesQuery(long roleId)
        {
            RoleId = roleId;
        }
    }
}
