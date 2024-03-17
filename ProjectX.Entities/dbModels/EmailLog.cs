using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class EmailLog
    {
        public Guid? IdEmailLog { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string DisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public bool IsSent { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsManual { get; set; }
        public int IdPolicy { get; set; }
    }
}
