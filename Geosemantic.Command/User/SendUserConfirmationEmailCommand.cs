using System;
using System.Collections.Generic;
using System.Text;
using Xen.Command;
using Xen.Common;

namespace Geosemantic.Command.User
{
   public class SendUserConfirmationEmailCommand : XenCommand<XenResult>
    {
        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public SendUserConfirmationEmailCommand()
        {

        }

        public SendUserConfirmationEmailCommand(XenMessage parentMsg)
        {
            MessageId = parentMsg.MessageId;
        }
    }
}
