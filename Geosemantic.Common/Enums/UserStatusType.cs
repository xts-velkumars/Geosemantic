using System.ComponentModel;

namespace Geosemantic.Common.Enums
{
    public enum UserStatusType : byte
    {
        [Description("Waiting For Approval")]
        WaitingForApproval = 1,

        [Description("Approved")]
        Approved,

        [Description("Rejected")]
        Rejected
    }
}
