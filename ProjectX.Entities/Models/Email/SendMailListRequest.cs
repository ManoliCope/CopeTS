using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Emails
{
    public class SendMailListRequest
    {
        public SendMailListRequest()

        {

            EmailRequestList = new List<SendEmailRequest>();

        }

        public string ApiKey { get; set; }

        public int ProjectId { get; set; }

        public int EmailId { get; set; }

        public List<SendEmailRequest> EmailRequestList { get; set; }
}
}
