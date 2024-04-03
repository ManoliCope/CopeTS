using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Transactions;
using ProjectX.Entities.Models.Emails;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ProjectX.Repository.EmailRepository
{
    public class EmailRepository : IEmailRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;
        public EmailRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }

        public SendMailListResponse SendEmail(SendMailListRequest request)
        {
            try
            {
                var resp = new SendMailListResponse();
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(new SendMailListRequest()
                {
                    ApiKey = _appSettings.EmailXMLSettings.ApiKey,
                    ProjectId = Convert.ToInt32(_appSettings.EmailXMLSettings.ProjectId),
                    EmailId = 1,
                    EmailRequestList = request.EmailRequestList
                });
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                client.BaseAddress = new Uri("http://172.27.8.219:4027/api/SendBulkEmails");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsync("", data).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseJsonString = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(responseJsonString))
                    {
                        resp = JsonConvert.DeserializeObject<SendMailListResponse>(responseJsonString);
                        //if (resp.Status == 1)
                        //{ 

                        //}
                    }
                }
                return resp;
            }
            catch (Exception e)
            {
                return null;
            }

        }



        public Exception SendMailNew(string subject, string body, List<MailAttachment> Attachments, string sender, string displayName, string receivers,
       string senderSMTP, int senderPort, string senderUsername, string senderPassword, bool enableSsl, string ccEmail,int emailID)
        {
           
            Exception _ex = null;
            System.Net.Mail.Attachment attachment = null;
            try
            {
                EmailModelDTO email = new EmailModelDTO();
                email.subject = subject;
                email.body = body;
                email.ApiKey = _appSettings.EmailXMLSettings.ApiKey;
                email.ProjectId = _appSettings.EmailXMLSettings.ProjectId;
                email.displayName = displayName;
                email.EmailId = emailID;
                if (Attachments != null)
                {
                    if (Attachments.Count > 0)
                    {
                        email.fileBytes = new List<byte[]>();
                        email.fileNames = new List<string>();

                        foreach (MailAttachment mailAttachment in Attachments)
                        {
                            if (!string.IsNullOrEmpty(mailAttachment.FilePath))
                            {
                                attachment = new System.Net.Mail.Attachment(mailAttachment.FilePath);
                                attachment.Name = mailAttachment.FileName;
                                byte[] thisbyte = streamtobyte(attachment.ContentStream);

                                email.fileBytes.Add(thisbyte);
                                email.fileNames.Add(attachment.Name);
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(receivers))
                    email.mailTo = receivers;

                if (!string.IsNullOrEmpty(ccEmail))
                    email.mailCC = ccEmail;

                _ex = sendxmlemail(email);
            }

            catch (SmtpException ex)
            {
                _ex = ex;
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            return _ex;
        }


        public void SaveEmailLog(EmailLog emailLog, int IdUser)
        {
            var param = new DynamicParameters();
            param.Add("@Sender", emailLog.Sender);
            param.Add("@Recipient", emailLog.Recipient);
            param.Add("@DisplayName", emailLog.DisplayName);
            param.Add("@Subject", emailLog.Subject);
            param.Add("@Body", emailLog.Body);
            param.Add("@IsSent", emailLog.IsSent);
            param.Add("@ErrorMessage", emailLog.ErrorMessage);
            param.Add("@IsManual", emailLog.IsManual);
            param.Add("@IdUser", IdUser);
            param.Add("@IdPolicy", emailLog.IdPolicy);
            using (TransactionScope scope = new TransactionScope())
            {
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                {
                    _db.Execute("tr_email_log_save", param, commandType: CommandType.StoredProcedure);
                }
                scope.Complete();
            }
        }

        public static byte[] streamtobyte(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public Exception sendxmlemail(EmailModelDTO email)
        {
            Exception _ex = null;
            var xmlSerliazer = new System.Xml.Serialization.XmlSerializer(typeof(EmailModelDTO));
            var stringWriter = new StringWriter();
            xmlSerliazer.Serialize(stringWriter, email);
            var xml = stringWriter.ToString();
            var xmlTrimmed = xml.Substring(xml.IndexOf("<mailTo>"));

            var xmlTrimmedFinal = xmlTrimmed.Substring(0, xmlTrimmed.Length - 16);

            var xmlWithEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap12:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap12=""http://www.w3.org/2003/05/soap-envelope"">
                <soap12:Body>
                <SendMail xmlns=""http://tempuri.org/"">
                {xmlTrimmedFinal}
                </SendMail>
                </soap12:Body>
                </soap12:Envelope>";

            var url = _appSettings.EmailXMLSettings.baseUri;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "text/xml";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(xmlWithEnvelope);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)request.GetResponse();

            EmailResponse emailResponse = new EmailResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                //read values from xml

                var xmlResult = result.Substring(result.IndexOf("<SendMailResult>") + 16);
                xmlResult = xmlResult.Substring(0, xmlResult.IndexOf("</SendMailResult>"));
                //extract the value from the xml
                var xmlResultTrimmed = xmlResult.Substring(xmlResult.IndexOf("<status>") + 8);
                xmlResultTrimmed = xmlResultTrimmed.Substring(0, xmlResultTrimmed.IndexOf("</status>"));
                emailResponse.status = Convert.ToInt32(xmlResultTrimmed);

                //extract the value from the xml
                xmlResultTrimmed = xmlResult.Substring(xmlResult.IndexOf("<message>") + 9);
                xmlResultTrimmed = xmlResultTrimmed.Substring(0, xmlResultTrimmed.IndexOf("</message>"));
                emailResponse.message = xmlResultTrimmed;
            }

            return null;
        }

        // create seperate classes
        public class EmailResponse
        {
            public int status { get; set; }
            public string message { get; set; }

        }

        public class EmailModelDTO
        {
            public string mailTo { get; set; }
            public string displayName { get; set; }
            public string subject { get; set; }
            public string IsBodyHtml { get; set; }
            public string body { get; set; }
            public string ApiKey { get; set; }
            public string ProjectId { get; set; }
            public List<byte[]> fileBytes { get; set; }
            public List<string> fileNames { get; set; }
            public string mailCC { get; set; }
            public string BCC { get; set; }
            public int EmailId { get; set; }

        }


        public async Task<bool> SendEmailSMTP(SendEmailRequest request)
        {
            // SMTP server settings
            string smtpServer = _appSettings.emailSettings.Host;
            int port = _appSettings.emailSettings.Port; // SMTP port (587 is commonly used for TLS/STARTTLS)
            string username = _appSettings.emailSettings.Username;
            string password = _appSettings.emailSettings.Pass;

            // Sender address
            string senderEmail = _appSettings.emailSettings.Sender;

            // Split email addresses by ';'
            string[] toEmails = request.MailTo.Split(';');
            string[] ccEmails = !string.IsNullOrEmpty(request.MailCC) ? request.MailCC.Split(';') : new string[0];

            // Create and configure the SMTP client
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, port))
            {
                smtpClient.EnableSsl = true; // Set to true if your SMTP server requires SSL/TLS
                smtpClient.Credentials = new NetworkCredential(username, password);

                // Loop through each recipient email address
                foreach (string toEmail in toEmails)
                {
                    // Create the email message for each recipient
                    MailMessage mailMessage = new MailMessage(senderEmail, toEmail.Trim(), request.Subject, request.Body);
                    if (ccEmails.Length > 0)
                    {
                        foreach (string ccEmail in ccEmails)
                        {
                            mailMessage.CC.Add(ccEmail.Trim());
                        }
                    }

                    if (request.FileBytes.Count > 0)
                    {
                        // Attachments
                        foreach (var attachmentData in request.FileBytes)
                        {
                            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(new MemoryStream(attachmentData), "New Policy.pdf");
                            mailMessage.Attachments.Add(attachment);
                        }
                    }

                    mailMessage.IsBodyHtml = true; // Set to true if your email body is HTML

                    try
                    {
                        // Send the email
                        await smtpClient.SendMailAsync(mailMessage);
                        var emailLog = new EmailLog
                        {
                            Sender = senderEmail,
                            Recipient = toEmail.Trim(),
                            Subject = request.Subject,
                            Body = request.Body,
                            IdPolicy = request.PolicyId ?? 0,
                            IsSent = true,
                            DisplayName = request.DisplayName ?? ""
                        };
                        SaveEmailLog(emailLog, request.UserID);
                    }
                    catch (Exception ex)
                    {
                        var emailLog = new EmailLog
                        {
                            Sender = senderEmail,
                            Recipient = toEmail.Trim(),
                            Subject = request.Subject,
                            Body = request.Body,
                            IdPolicy = request.PolicyId ?? 0,
                            IsSent = false,
                            DisplayName = request.DisplayName ?? "",
                            ErrorMessage = ex.Message
                        };
                        SaveEmailLog(emailLog, request.UserID);
                        return false;
                    }
                }
            }

            return true;
        }
    }
}