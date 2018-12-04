using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Geosemantic.Command.Email;
using Geosemantic.Common;
using Geosemantic.Queries.User;
using Geosemantic.ViewModel;
using MediatR;
using Xen.Common;

namespace Geosemantic.Command.User
{
    public class SendUserRegistrationEmailCommandHandler : IRequestHandler<SendUserRegistrationEmailCommand, XenResult>
    {
        private readonly IMediator mediatr;
        private readonly EmailSettings emailSettings;
        private readonly EmailTemplates templates;


        public SendUserRegistrationEmailCommandHandler(IMediator mediatr, EmailSettings emailSettings, EmailTemplates templates)
        {
            this.mediatr = mediatr;
            this.emailSettings = emailSettings;
            this.templates = templates;
        }

        public async Task<XenResult> Handle(SendUserRegistrationEmailCommand command, CancellationToken token)
        {
            var adminEmail = await mediatr.Send(new SuperAdminEmailAddressQuery(), token);

            if (string.IsNullOrEmpty(adminEmail))
                return new SuccessResult();

            await SendEmail(command, adminEmail);
            return new SuccessResult();
        }

        private async Task SendEmail(SendUserRegistrationEmailCommand command, string toEmailAddress)
        {
            var agentConfirmationEmailViewModel = Mapper.Map<UserConfirmationEmailViewModel>(command);
            var messageBody = templates.GetUserRegistrationEmailBody(agentConfirmationEmailViewModel);
            var mailMessage = new MailMessage(emailSettings.SmtpFrom, toEmailAddress)
            {
                Subject = "New User Registration",
                Body = messageBody,
                IsBodyHtml = true
            };

            mailMessage.Attachments.Add(templates.GetLogoAttachment());

            await mediatr.Send(new SendEmailWithSendGridCommand(command, mailMessage));
            //await mediatr.Send(new SendEmailCommand(command, mailMessage));
        }
    }
}
