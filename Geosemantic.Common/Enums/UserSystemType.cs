using System.ComponentModel;

namespace Geosemantic.Common.Enums
{
    public enum UserSystemType : byte
    {
        [Description("System")]
        System = 1,

        [Description("UserDefined")]
        UserDefined
    }
}
