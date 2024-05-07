using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrEmailLog
    {
        public Guid ElId { get; set; }
        public string? ElSender { get; set; }
        public string? ElRecipient { get; set; }
        public string? ElDisplayName { get; set; }
        public string? ElSubject { get; set; }
        public string? ElBody { get; set; }
        public DateTime ElCreated { get; set; }
        public int? ElCreatedBy { get; set; }
        public bool ElIsManual { get; set; }
        public bool ElIsSent { get; set; }
        public string? ElErrorMessage { get; set; }
        public int? PolicyId { get; set; }
    }
}
