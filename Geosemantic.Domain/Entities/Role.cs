using System.Collections.Generic;
using Geosemantic.Common.Enums;
using Xen.Entity.Entities;

namespace Geosemantic.Domain.Entities
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            RolePageMapping = new HashSet<RolePageMapping>();
        }

        public string Name { get; set; }

        public ICollection<User> User { get; set; }

        public RoleSystemType RoleSystemType { get; set; }

        public ICollection<RolePageMapping> RolePageMapping { get; set; }

    }
}
