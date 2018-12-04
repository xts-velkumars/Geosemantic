using Geosemantic.Common.Constants;
using Geosemantic.Common.Enums;
using Xen.Common.Interface;

namespace Geosemantic.Domain.Entities
{
    public partial class Role
    {
        public bool IsSystemRole => RoleSystemType == RoleSystemType.System;

        public string CreatedDate => CreationTs.ToString(ApplicationContants.DateTimeFormat);

        public void ModifyDatesToDisplay(ILoggedOnUserProvider user)
        {
            CreationTs = user.DisplayUserTimeFromUtc(CreationTs);
        }
    }
}
