using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrEmailTemplate
    {
        public string EtCode { get; set; } = null!;
        public string? EtTitle { get; set; }
        public string? EtSubject { get; set; }
        public string? EtBody { get; set; }
        public bool EtWithAttachment { get; set; }
        public DateTime EtCreated { get; set; }
        public DateTime? EtUpdated { get; set; }
        public string? EtRecepients { get; set; }
        public string? EtLob { get; set; }
    }
}
