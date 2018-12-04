using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Geosemantic.Command.Email;
using Geosemantic.Common;
using MediatR;
using Xen.Common;
using Xen.Extensions;

namespace Geosemantic.Command.User
{
    public class SendUserConfirmationEmailCommandHandler : IRequestHandler<SendUserConfirmationEmailCommand, XenResult>
    {
        private readonly IMediator mediatr;
        private readonly EmailSettings emailSettings;
        private readonly EmailTemplates templates;

        public SendUserConfirmationEmailCommandHandler(IMediator mediatr, EmailSettings emailSettings, EmailTemplates templates)
        {
            this.mediatr = mediatr;
            this.emailSettings = emailSettings;
            this.templates = templates;
        }

        public async Task<XenResult> Handle(SendUserConfirmationEmailCommand command, CancellationToken token)
        {
            if (!command.UserEmail.HasValue())
                return new SuccessResult();

            await SendEmail(command, command.UserEmail);
            return new SuccessResult();
        }


        private async Task SendEmail(SendUserConfirmationEmailCommand command, string toEmailAddress)
        {
            var messageBody = templates.GetAgentConfirmationEmailBody(command.UserName);
            var mailMessage = new MailMessage(emailSettings.SmtpFrom, toEmailAddress)
            {
                Subject = "Agent Confirmation",
                Body = messageBody,
                IsBodyHtml = true
            };

            mailMessage.Attachments.Add(templates.GetLogoAttachment());
          
            await mediatr.Send(new SendEmailCommand(command, mailMessage));
        }
    }
}
