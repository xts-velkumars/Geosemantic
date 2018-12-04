using Geosemantic.Common.Enums;
using Xen.Command;
using Xen.Common;

namespace Geosemantic.Command.User
{
    public class SaveUserCommand : XenCommand<XenResult<bool>>
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public GenderType GenderType { get; set; }

        public UserSystemType UserSystemType { get; set; }

        public string DateOfBirth { get; set; }

        public string MobileNumber { get; set; }
     
        public long RoleId { get; set; }
    }
}
