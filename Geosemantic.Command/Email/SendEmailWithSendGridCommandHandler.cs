using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Geosemantic.Common;
using MediatR;
using SendGrid;
using SendGrid.Helpers.Mail;
using Xen.Common;
using Xen.Extensions;

namespace Geosemantic.Command.Email
{
    public class SendEmailWithSendGridCommandHandler : IRequestHandler<SendEmailWithSendGridCommand, XenResult>
    {
        private readonly AppSettings appSettings;


        public SendEmailWithSendGridCommandHandler(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public async Task<XenResult> Handle(SendEmailWithSendGridCommand command, CancellationToken token)
        {
            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress(command.MailMessage.From.Address),
                Subject = command.MailMessage.Subject,
                HtmlContent = command.MailMessage.Body
            };

            foreach (var toEmailAddress in command.MailMessage.To)
            {
                sendGridMessage.AddTo(toEmailAddress.Address);
            }

            foreach (var attachment in command.MailMessage.Attachments)
            {
                sendGridMessage.AddAttachment(attachment.ContentType.Name,
                    Convert.ToBase64String(attachment.ContentStream.ToBytes()), attachment.ContentType.MediaType,
                    attachment.ContentDisposition.DispositionType, attachment.ContentType.Name);
            }

            var apiKey = appSettings.SendGridApiKey;
            var client = new SendGridClient(apiKey);
            var response = await client.SendEmailAsync(sendGridMessage, token);

            if (response.StatusCode != HttpStatusCode.Accepted)
                return new FailureResult(response.StatusCode.ToString());
        
            return new SuccessResult();

        }
    }
}
