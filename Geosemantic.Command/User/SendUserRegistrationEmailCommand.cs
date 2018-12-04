using Xen.Command;
using Xen.Common;

namespace Geosemantic.Command.User
{
    public class SendUserRegistrationEmailCommand : XenCommand<XenResult>
    {
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }

        public SendUserRegistrationEmailCommand()
        {

        }

        public SendUserRegistrationEmailCommand(XenMessage parentMsg)
        {
            MessageId = parentMsg.MessageId;
        }
    }
}