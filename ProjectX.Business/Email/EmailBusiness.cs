using ProjectX.Entities;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using ProjectX.Repository.EmailRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Utilities;

namespace ProjectX.Business.Email
{
    public class EmailBusiness : IEmailBusiness
    {
        IEmailRepository _emailRepository;

        public EmailBusiness(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public bool SendEmail(string subject, string body, List<MailAttachment> attachments, string sender, string displayName, string receivers,
            string senderSMTP, int senderPort, string senderUsername, string senderPassword, bool enableSsl, string ccEmail, bool IsManual, int IdUser, int IdCase, string LOB)
        {
            bool isSent = false;
            try
            {
                Exception exception = null;
                string errorMessage = string.Empty;

                if (!body.Contains("***"))
                {
                    if (IsManual)
                        body = "<p>" + body + "</p>";

                    var emailID = 0;
                    if (LOB == "TR")
                    {
                        emailID = 3;
                    }
                    //exception = EmailManager.SendMailNew(subject, body, attachments, sender, displayName, receivers, senderSMTP, senderPort, senderUsername, senderPassword, enableSsl, ccEmail);
                    exception = _emailRepository.SendMailNew(subject, body, attachments, sender, displayName, receivers, senderSMTP, senderPort, senderUsername, senderPassword, enableSsl, ccEmail,emailID);

                    if (exception == null)
                        isSent = true;
                    else
                        errorMessage = exception.Message;
                }
                else
                    errorMessage = "Invalid email body";
                

                EmailLog emailLog = new EmailLog
                {
                    Subject = subject,
                    Body = body,
                    Date = DateTime.Now.Date.ToString("dd-MM-yyyy"),
                    DisplayName = displayName,
                    IsSent = isSent,
                    ErrorMessage = errorMessage,
                    Recipient = receivers,
                    Sender = sender,
                    IsManual = IsManual,
                    IdCase = IdCase
                };
                _emailRepository.SaveEmailLog(emailLog, IdUser);
            }
            catch (Exception ex) {
                string errorMessage = ex.Message;

                EmailLog emailLog = new EmailLog
                {
                    Subject = subject,
                    Body = body,
                    Date = DateTime.Now.Date.ToString("dd-MM-yyyy"),
                    DisplayName = displayName,
                    IsSent = isSent,
                    ErrorMessage = errorMessage,
                    Recipient = receivers,
                    Sender = sender,
                    IsManual = IsManual,
                    IdCase = IdCase
                };
                _emailRepository.SaveEmailLog(emailLog, IdUser);
            }
            return isSent;
        }
    }
}
