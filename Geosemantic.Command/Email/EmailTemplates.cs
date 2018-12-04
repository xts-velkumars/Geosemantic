using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using Geosemantic.Common.Constants;
using Geosemantic.ViewModel;

namespace Geosemantic.Command.Email
{
    public class EmailTemplates
    {
        private static string EmailTemplateResourcePathRoot => "Geosemantic.Command.Email.Templates.";


        public string GetUserRegistrationEmailBody(UserConfirmationEmailViewModel model)
        {
            var template = GetEmailTemplateFileContents("Users.UserRegistration.html");

            return ReplaceTemplateTokens(template, new[]
            {
                Token.UserName(model.UserName),
                Token.MobileNumber(model.MobileNumber),
                Token.Email(model.Email),
                Token.DateOfBirth(model.DateOfBirth.ToString(ApplicationContants.DateTimeFormat)),
            });
        }

        public string GetAgentConfirmationEmailBody(string agentName)
        {
            var template = GetEmailTemplateFileContents("Users.UserConfirmation.html");

            return ReplaceTemplateTokens(template, new[]
            {
                Token.UserName(agentName)
            });
        }
        
        private static string GetEmailTemplateFileContents(string templatePath)
        {
            using (var stream = GetEmbeddedResourceStream(EmailTemplateResourcePathRoot + templatePath))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static Stream GetEmbeddedResourceStream(string resourcePath)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        }

        private static string ReplaceTemplateTokens(string template, IEnumerable<Token> tokens)
        {
            foreach (var token in tokens)
            {
                template = template.Replace(token.Key, token.Value);
            }
            return template;
        }

        public Attachment GetLogoAttachment()
        {
            var att = new Attachment(GetEmbeddedResourceStream(EmailTemplateResourcePathRoot + "Images.logo.png"), new ContentType("image/png"));
            att.ContentDisposition.Inline = true;
            att.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            att.ContentId = "logo";
            att.ContentType.MediaType = "image/png";
            att.ContentType.Name = "logo.png";
            return att;
        }

        private class Token
        {
            private Token(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; }
            public string Value { get; }


            #region Agent Confirmation Email

            public static Token ApplicantName(string value)
            {
                return new Token("##APPLICANTNAME##", value);
            }

            public static Token UserName(string value)
            {
                return new Token("##USERNAME##", value);
            }

           

            public static Token MobileNumber(string value)
            {
                return new Token("##MOBILENUMBER##", value);
            }

            public static Token Email(string value)
            {
                return new Token("##EMAIL##", value);
            }

            public static Token DateOfBirth(string value)
            {
                return new Token("##DOB##", value);
            }

            #endregion Agent Confirmation Email
        }
    }
}
