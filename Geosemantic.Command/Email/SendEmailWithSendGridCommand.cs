using System.Net.Mail;
using Newtonsoft.Json;
using Xen.Command;
using Xen.Common;
using Xen.Common.Interface;

namespace Geosemantic.Command.Email
{
    public class SendEmailWithSendGridCommand : XenCommand<XenResult>
    {
        [JsonIgnore]
        public MailMessage MailMessage { get; set; }

        public SendEmailWithSendGridCommand()
        {

        }

        public SendEmailWithSendGridCommand(XenMessage parentMsg, MailMessage mailMessage)
        {
            MessageId = parentMsg.MessageId;
            MailMessage = mailMessage;
        }

        public SendEmailWithSendGridCommand(XenMessage parentMsg, ILoggedOnUserProvider user, MailMessage mailMessage)
        {
            MessageId = parentMsg.MessageId;
            SetUser(user);
            MailMessage = mailMessage;
        }
    }
}
