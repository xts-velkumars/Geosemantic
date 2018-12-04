using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Geosemantic.Common;
using MediatR;
using Xen.Common;

namespace Geosemantic.Command.Email
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, XenResult>
    {
        private readonly EmailSettings emailSettings;


        public SendEmailCommandHandler(EmailSettings emailSettings)
        {
            this.emailSettings = emailSettings;
        }

        public async Task<XenResult> Handle(SendEmailCommand command, CancellationToken token)
        {
            using (var smtp = new SmtpClient(emailSettings.SmtpServer)
            {
                Port =  emailSettings.SmtpPort,
                EnableSsl = true
            })
            {
                smtp.Credentials = new NetworkCredential(emailSettings.SmtpUsername, emailSettings.SmtpPassword);
                smtp.ServicePoint.MaxIdleTime = 1;

                await smtp.SendMailAsync(command.MailMessage);
                return new SuccessResult();
            }
        }
    }
}
