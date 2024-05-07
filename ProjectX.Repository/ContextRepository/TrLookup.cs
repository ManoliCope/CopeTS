using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrLookup
    {
        public int LkIdLookup { get; set; }
        public string LkTableName { get; set; } = null!;
        public int LkId { get; set; }
        public string LkTableField { get; set; } = null!;
        public short LkSort { get; set; }
        public string? LkIcon { get; set; }
        public string? LkLob { get; set; }
        public bool? LkIsActive { get; set; }
        public DateTime LkCreated { get; set; }
    }
}
