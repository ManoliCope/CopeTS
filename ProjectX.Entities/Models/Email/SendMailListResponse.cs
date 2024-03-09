using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Emails
{
    public class SendMailListResponse

    {

        public int Status { get; set; }

        public string Message { get; set; }

        public List<EmailResult> EmailResultList { get; set; }

    }

   
}
