using ProjectX.Business.Caching;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Emails;
using ProjectX.Repository.EmailRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Business.Email
{
    public class EmailBusiness : IEmailBusiness
    {
        private IList<TextReplacement> _textReplacements;
        private IList<EmailTemplate> _emailTemplates;
        IEmailRepository _emailsRepository;
        private IDatabaseCaching _databaseCaching;


        public EmailBusiness(IEmailRepository emailsRepository, IDatabaseCaching databaseCaching)
        {
            _emailsRepository = emailsRepository;
            _databaseCaching = databaseCaching;
            _textReplacements = _databaseCaching.GetTextReplacements();
            _emailTemplates = _databaseCaching.GetEmailTemplates();
        }

        public async void ToSendEmail(object dataObject, string emailTemplateCode,byte[] attachment)
        {
            //PolicyNotification
            string displayName = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string recipients = string.Empty;
            string ccrecipients = string.Empty;

            // Assume _emailTemplates is a list of EmailTemplate objects
            EmailTemplate emailTemplate = _emailTemplates.FirstOrDefault(x => x.Code == emailTemplateCode);

            if (emailTemplate != null)
            {
                subject = emailTemplate.subject;
                body = ReplaceBody(dataObject, emailTemplate.body);
                recipients = emailTemplate.recepients;
                //ccrecipients ="moussa.basma@securiteassurance.com";

                var attachmentList =new List<byte[]>();
                if (emailTemplate.WithAttachment==true)
                {
                    attachmentList.Add(attachment);
                }

                SendMailListRequest request = new SendMailListRequest();
                SendEmailRequest email = new SendEmailRequest
                {
                    MailTo = recipients,
                    //DisplayName = "",
                    Subject = subject,
                    Body = body,
                    MailCC = ccrecipients,
                    FileBytes= attachmentList
                };

                request.EmailRequestList.Add(email);
                await _emailsRepository.SendEmailSMTP(email);

                //_emailsRepository.SendEmail(request);
            }

            // Reset variables
            subject = string.Empty;
            body = string.Empty;
            recipients = string.Empty;
        }
        public string ReplaceBody(object dataObject, string body)
        {
            foreach (TextReplacement textReplacement in _textReplacements)
            {
                switch (textReplacement.Code)
                {
                    case "***InvoiceNumber***":
                        // Assuming dataObject has a property named "InvoiceNumber"
                        body = body.Replace(textReplacement.Code, dataObject.GetType().GetProperty("invoice_number")?.GetValue(dataObject)?.ToString() ?? "");
                        break;

                    case "***Supplier***":
                        // Assuming dataObject has a property named "Supplier"
                        body = body.Replace(textReplacement.Code, dataObject.GetType().GetProperty("beneficiary")?.GetValue(dataObject)?.ToString() ?? "");
                        break;

                    case "***Amount***":
                        // Assuming dataObject has a property named "Amount"
                        body = body.Replace(textReplacement.Code, dataObject.GetType().GetProperty("total_amount")?.GetValue(dataObject)?.ToString() ?? "");
                        break;

                    case "***PaymentMode***":
                        // Assuming dataObject has a property named "PaymentMode"
                        body = body.Replace(textReplacement.Code, dataObject.GetType().GetProperty("total_amount_currency")?.GetValue(dataObject)?.ToString() ?? "");
                        break;

                    default:
                        break;
                }
            }
            return body;
        }
        //public async void SendPolicyByEmail(int policyId)
        //{
            
                
        //}
           public async Task<bool> SendPolicyByEmail(string to,string cc, string emailTemplateCode,byte[] attachment, object? dataObject)
        {
            //PolicyNotification
            string displayName = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string recipients = string.Empty;
            string ccrecipients = string.Empty;
            var success = false;
            // Assume _emailTemplates is a list of EmailTemplate objects
            EmailTemplate emailTemplate = _emailTemplates.FirstOrDefault(x => x.Code == emailTemplateCode);

            if (emailTemplate != null)
            {
                subject = emailTemplate.subject;
                //body = ReplaceBody(dataObject, emailTemplate.body);
                body= emailTemplate.body;
                if(dataObject!=null)
                    body = ReplaceBody(dataObject, emailTemplate.body);

                recipients = to;
                ccrecipients = cc;
                //ccrecipients ="moussa.basma@securiteassurance.com";

                var attachmentList =new List<byte[]>();
                if (emailTemplate.WithAttachment==true)
                {
                    attachmentList.Add(attachment);
                }

                SendMailListRequest request = new SendMailListRequest();
                SendEmailRequest email = new SendEmailRequest
                {
                    MailTo = recipients,
                    //DisplayName = "",
                    Subject = subject,
                    Body = body,
                    MailCC = ccrecipients,
                    FileBytes= attachmentList
                };

                request.EmailRequestList.Add(email);
                success= await _emailsRepository.SendEmailSMTP(email);

                //_emailsRepository.SendEmail(request);
            }

            // Reset variables
            subject = string.Empty;
            body = string.Empty;
            recipients = string.Empty;
            return success;
        }
    }
}
