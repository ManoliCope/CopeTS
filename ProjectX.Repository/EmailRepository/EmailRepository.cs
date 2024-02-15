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
            param.Add("@IdCase", emailLog.IdCase);
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
    }
}