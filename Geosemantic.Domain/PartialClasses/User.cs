using Geosemantic.Common.Enums;

namespace Geosemantic.Domain.Entities
{
    public partial class User
    {
        public string FullName => $"{FirstName} {LastName}";
        public string RoleName => Role?.Name;
        public bool IsSystemUser => UserSystemType == UserSystemType.System;
        public string Status => UserStatusType.ToString();
    }
}
