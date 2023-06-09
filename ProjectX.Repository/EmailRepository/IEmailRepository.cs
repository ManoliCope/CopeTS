﻿using ProjectX.Entities;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.EmailRepository
{
    public interface IEmailRepository
    {
        void SaveEmailLog(EmailLog emailLog, int IdUser);
          
        Exception SendMailNew(string subject, string body, List<MailAttachment> Attachments, string sender, string displayName, string receivers,
          string senderSMTP, int senderPort, string senderUsername, string senderPassword, bool enableSsl, string ccEmail,int emailID);
    
    }
}