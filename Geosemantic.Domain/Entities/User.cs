using Geosemantic.Common.Enums;
using Xen.Entity.Entities;

namespace Geosemantic.Domain.Entities
{
    public partial class User : BaseEntity
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public GenderType? GenderType { get; set; }
    }
}
