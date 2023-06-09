using ProjectX.Entities;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Email
{
    public interface IEmailBusiness
    {
        bool SendEmail(string subject, string body, List<MailAttachment> AttachmentPaths, string sender, string displayName, string receivers,
            string senderSMTP, int senderPort, string senderUsername, string senderPassword, bool enableSsl, string ccEmail, bool IsManual, int IdUser, int IdCase,string LOB);
    }
}
