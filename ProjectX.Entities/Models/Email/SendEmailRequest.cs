using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Emails
{
    public class SendEmailRequest

    {
        public int UserID { get; set; }
        public int? PolicyId { get; set; }

        public string MailTo { get; set; }

        public string? DisplayName { get; set; }

        public string Subject { get; set; }

        public bool IsBodyHtml { get; set; }

        public string Body { get; set; }

        public List<byte[]>? FileBytes { get; set; }

        public List<string>? FileNames { get; set; }

        public string? MailCC { get; set; }

        public string? BCC { get; set; }

        public string? AlternateView { get; set; }

        public string? LogoName { get; set; }

        public string? ContentId { get; set; }

        public string? LogoMediaType { get; set; }

        public string? AVMediaType { get; set; }

    }
}
