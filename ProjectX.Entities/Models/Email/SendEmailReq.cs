using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Emails
{
    public class SendEmailReq
    {
        public string sender { get; set; }
        public string recipients { get; set; }
        public string ccRecipients { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool withAttachments { get; set; }
        public string userName { get; set; }
    }
}
