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
                Token.AgentName(model.UserName),
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
                Token.AgentName(agentName)
            });
        }

        //public string GetAgentRejectionConfirmationEmailBody(string agentName)
        //{
        //    var template = GetEmailTemplateFileContents("User.UserRejection.html");

        //    return ReplaceTemplateTokens(template, new[]
        //    {
        //        Token.AgentName(agentName)
        //    });
        //}

        //public string GetLoanApplicationEmailBody(LoanApplicationEmailViewModel model)
        //{
        //    var template = GetEmailTemplateFileContents("LoanApplication.LoanApplication.html");

        //    return ReplaceTemplateTokens(template, new[]
        //    {
        //        Token.ApplicantName(model.ApplicantName),
        //        Token.AgentName(model.AgentName),
        //        Token.AgentReferenceNumber(model.AgentReferenceNumber),
        //        Token.MobileNumber(model.MobileNumber),
        //        Token.Email(model.Email),
        //        Token.DateOfBirth(model.DateOfBirth.ToString(ApplicationContants.DateTimeFormat)),
        //        Token.Address(model.Address),
        //        Token.State(model.State),
        //        Token.District(model.District),
        //        Token.City(model.City),
        //        Token.PinCode(model.PinCode),
        //        Token.LoanType(model.LoanType),
        //        Token.LoanAmount(model.LoanAmount),
        //        Token.LoanTenure(model.LoanTenure)
        //    });
        //}

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

            public static Token AgentName(string value)
            {
                return new Token("##AGENTNAME##", value);
            }

            public static Token AgentReferenceNumber(string value)
            {
                return new Token("##AGENTREFERENCENUMBER##", value);
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

            public static Token AverageTurnOver(string value)
            {
                return new Token("##TURNOVER##", value);
            }

            public static Token Address(string value)
            {
                return new Token("##ADDRESS##", value);
            }

            public static Token State(string value)
            {
                return new Token("##STATE##", value);
            }

            public static Token District(string value)
            {
                return new Token("##DISTRICT##", value);
            }

            public static Token City(string value)
            {
                return new Token("##CITY##", value);
            }

            public static Token PinCode(string value)
            {
                return new Token("##PINCODE##", value);
            }

            public static Token LoanType(string value)
            {
                return new Token("##LOANTYPE##", value);
            }

            public static Token LoanAmount(string value)
            {
                return new Token("##LOANAMOUNT##", value);
            }

            public static Token LoanTenure(string value)
            {
                return new Token("##LOANTENURE##", value);
            }



            #endregion Agent Confirmation Email
        }
    }
}
