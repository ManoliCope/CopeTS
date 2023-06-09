using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilities
{
    public static class EmailManager
    {
        public static bool IsValidEmail(object email)
        {
            bool result = true;
            try
            {
                if (email != null)
                {
                    char[] delimiter = { ',', ';' };
                    string[] validMail = email.ToString().Split(delimiter);
                    for (int i = 0; i < validMail.Length; i++)
                        if (!Regex.IsMatch(validMail[i], @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                            result = false;
                }
                else
                    return false;
            }
            catch
            {
            }
            return result;
        }

        public static bool SendMail(string subject, string body, string AttachmentPath, string sender, string displayName, string receivers,
            string senderSMTP, int senderPort, string senderPassword, bool enableSsl, string ccEmail)
        {
            bool Result = false;
            try
            {
                System.Net.Mail.MailMessage mailmsg = new System.Net.Mail.MailMessage();
                mailmsg.Subject = subject;
                mailmsg.Body = body;
                mailmsg.IsBodyHtml = true;
                mailmsg.From = new System.Net.Mail.MailAddress(sender, displayName);

                if (!string.IsNullOrEmpty(AttachmentPath))
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(AttachmentPath);
                    mailmsg.Attachments.Add(attachment);
                }

                string[] DestinationEmails = receivers.Split(';');
                foreach (string mail in DestinationEmails)
                    if (!string.IsNullOrEmpty(mail))
                        mailmsg.To.Add(mail);
                if (!string.IsNullOrEmpty(ccEmail))
                    mailmsg.CC.Add(ccEmail);

                System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient();
                smtpclient.Host = senderSMTP;
                smtpclient.Port = senderPort;
                smtpclient.EnableSsl = enableSsl;

                smtpclient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpclient.Credentials = new NetworkCredential(sender, senderPassword);
                smtpclient.UseDefaultCredentials = false;
                //smtpclient.TargetName = "STARTTLS/smtp.office365.com";
                // smtpclient.Timeout = 600000;

                //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                //        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                //        System.Security.Cryptography.X509Certificates.X509Chain chain,
                //        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                //{
                //    return true;
                //};

                smtpclient.Send(mailmsg);
                //if (attachment != null)
                //{
                //    attachment.Dispose();
                //}
                Result = true;
            }
            catch
            {
            }
            return Result;
        }

        public static bool SendMail(string SenderEmail, System.Net.Mail.MailMessage MailMessage, string Recipients, string ccRecipients, string SenderSMTP, int SenderPort, string SenderPassword, bool EnableSsl)
        {
            bool Result = false;
            try
            {
                string[] DestinationEmails = Recipients.Split(';');
                foreach (string mail in DestinationEmails)
                    if (!string.IsNullOrEmpty(mail))
                        MailMessage.To.Add(mail);
                if (!string.IsNullOrEmpty(ccRecipients))
                    MailMessage.CC.Add(ccRecipients);


                System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient();
                smtpclient.Host = SenderSMTP;
                smtpclient.Port = SenderPort;
                smtpclient.EnableSsl = EnableSsl;

                smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential(SenderEmail, SenderPassword);
                smtpclient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpclient.Timeout = 200000;
                //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                //        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                //        System.Security.Cryptography.X509Certificates.X509Chain chain,
                //        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                //{
                //    return true;
                //};

                smtpclient.Send(MailMessage);
                Result = true;
            }
            catch
            {
            }
            return Result;
        }

        // sending mail from sql
        public static Exception SendMailNew(string subject, string body, List<MailAttachment> Attachments, string sender, string displayName, string receivers,
            string senderSMTP, int senderPort, string senderUsername, string senderPassword, bool enableSsl, string ccEmail)
        {






            //receivers = "Hussein.Hachem@securiteassurance.com";
            Exception _ex = null;
            System.Net.Mail.MailMessage mailmsg = null;
            System.Net.Mail.Attachment attachment = null;
            System.Net.Mail.SmtpClient smtpclient = null;

            try
            {
                 mailmsg = new System.Net.Mail.MailMessage();
                
                mailmsg.Subject = subject;
                mailmsg.Body = body;
                mailmsg.IsBodyHtml = true;
                mailmsg.From = new System.Net.Mail.MailAddress(sender, displayName);

                if (Attachments != null)
                {
                    if (Attachments.Count > 0)
                    {
                        foreach (MailAttachment mailAttachment in Attachments)
                        {
                            if (!string.IsNullOrEmpty(mailAttachment.FilePath))
                            {
                                attachment = new System.Net.Mail.Attachment(mailAttachment.FilePath);
                                attachment.Name = mailAttachment.FileName;
                                mailmsg.Attachments.Add(attachment);
                            }
                        }
                    }
                }

                string[] DestinationEmails = receivers.Split(';');
                foreach (string mail in DestinationEmails)
                    if (!string.IsNullOrEmpty(mail))
                        mailmsg.To.Add(mail);
                if (!string.IsNullOrEmpty(ccEmail))
                    mailmsg.CC.Add(ccEmail);

                smtpclient = new System.Net.Mail.SmtpClient();

                //smtpclient.Host = senderSMTP;
                //smtpclient.Port = senderPort;
                //smtpclient.EnableSsl = enableSsl;

                //smtpclient.UseDefaultCredentials = false;
                //smtpclient.Credentials = new NetworkCredential(senderUsername, senderPassword);
                //smtpclient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtpclient.Timeout = 200000;




                smtpclient.Host = "192.168.0.5";
                smtpclient.Port = 25;
                //smtpclient.EnableSsl = true;
                //smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential("securiteassuran/mail.it", "emoticons?");
                //smtpclient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpclient.Timeout = 200000;


                //smtpclient.Host = "mail.securiteassurance.com";
                //smtpclient.Port = 25;
                //smtpclient.EnableSsl = true;

                //smtpclient.UseDefaultCredentials = false;
                //smtpclient.Credentials = new NetworkCredential("it.test@securiteassurance.com", "Password@123");
                //smtpclient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtpclient.Timeout = 200000;


                //smtpclient.TargetName = "STARTTLS/smtp.office365.com";

                //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                //        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                //        System.Security.Cryptography.X509Certificates.X509Chain chain,
                //        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                //{
                //    return true;
                //};

                smtpclient.Send(mailmsg);
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                if (mailmsg != null)
                    mailmsg.Dispose();
                if (attachment != null)
                    attachment.Dispose();
                if (smtpclient != null)
                    smtpclient.Dispose();
            }
            return _ex;
        }
        
        
        public static Exception SendMailNewbackup(string subject, string body, List<MailAttachment> Attachments, string sender, string displayName, string receivers,
            string senderSMTP, int senderPort, string senderUsername, string senderPassword, bool enableSsl, string ccEmail)
        {
            //receivers = "Hussein.Hachem@securiteassurance.com";
            Exception _ex = null;
            System.Net.Mail.MailMessage mailmsg = null;
            System.Net.Mail.Attachment attachment = null;
            System.Net.Mail.SmtpClient smtpclient = null;

            try
            {
                 mailmsg = new System.Net.Mail.MailMessage();
                
                mailmsg.Subject = subject;
                mailmsg.Body = body;
                mailmsg.IsBodyHtml = true;
                mailmsg.From = new System.Net.Mail.MailAddress(sender, displayName);

                if (Attachments != null)
                {
                    if (Attachments.Count > 0)
                    {
                        foreach (MailAttachment mailAttachment in Attachments)
                        {
                            if (!string.IsNullOrEmpty(mailAttachment.FilePath))
                            {
                                attachment = new System.Net.Mail.Attachment(mailAttachment.FilePath);
                                attachment.Name = mailAttachment.FileName;
                                mailmsg.Attachments.Add(attachment);
                            }
                        }
                    }
                }

                string[] DestinationEmails = receivers.Split(';');
                foreach (string mail in DestinationEmails)
                    if (!string.IsNullOrEmpty(mail))
                        mailmsg.To.Add(mail);
                if (!string.IsNullOrEmpty(ccEmail))
                    mailmsg.CC.Add(ccEmail);

                smtpclient = new System.Net.Mail.SmtpClient();

                //smtpclient.Host = senderSMTP;
                //smtpclient.Port = senderPort;
                //smtpclient.EnableSsl = enableSsl;

                //smtpclient.UseDefaultCredentials = false;
                //smtpclient.Credentials = new NetworkCredential(senderUsername, senderPassword);
                //smtpclient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtpclient.Timeout = 200000;




                smtpclient.Host = "192.168.0.5";
                smtpclient.Port = 25;
                //smtpclient.EnableSsl = true;
                //smtpclient.UseDefaultCredentials = false;
                smtpclient.Credentials = new NetworkCredential("securiteassuran/mail.it", "emoticons?");
                //smtpclient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpclient.Timeout = 200000;


                //smtpclient.Host = "mail.securiteassurance.com";
                //smtpclient.Port = 25;
                //smtpclient.EnableSsl = true;

                //smtpclient.UseDefaultCredentials = false;
                //smtpclient.Credentials = new NetworkCredential("it.test@securiteassurance.com", "Password@123");
                //smtpclient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtpclient.Timeout = 200000;


                //smtpclient.TargetName = "STARTTLS/smtp.office365.com";

                //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                //        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                //        System.Security.Cryptography.X509Certificates.X509Chain chain,
                //        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                //{
                //    return true;
                //};

                smtpclient.Send(mailmsg);
            }
            catch (Exception ex)
            {
                _ex = ex;
            }
            finally
            {
                if (mailmsg != null)
                    mailmsg.Dispose();
                if (attachment != null)
                    attachment.Dispose();
                if (smtpclient != null)
                    smtpclient.Dispose();
            }
            return _ex;
        }
    }
}
