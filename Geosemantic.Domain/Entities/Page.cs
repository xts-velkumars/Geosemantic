using System.Collections.Generic;
using Xen.Entity.Entities;

namespace Geosemantic.Domain.Entities
{
    public partial class Page : BaseEntity
    {
        public Page()
        {
            RolePageMapping = new HashSet<RolePageMapping>();
        }

        public string Name { set; get; }

        public string ShortName { set; get; }

        public long Sequence { set; get; }

        public string Url { set; get; }
        public long ParentId { set; get; }

        public string Icon { set; get; }

        public string Label { set; get; }

        public string HasBadge { set; get; }

        public string BadgeText { set; get; }

        public ICollection<RolePageMapping> RolePageMapping { get; set; }

    }
}
