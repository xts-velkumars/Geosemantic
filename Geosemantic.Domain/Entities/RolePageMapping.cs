using System;
using System.Collections.Generic;
using System.Text;
using Xen.Entity.Entities;

namespace Geosemantic.Domain.Entities
{
    public partial class RolePageMapping : BaseEntity
    {
        public long PageId { get; set; }

        public long RoleId { get; set; }

        public Page Page { get; set; }

        public Role Role { get; set; }
    }
}
