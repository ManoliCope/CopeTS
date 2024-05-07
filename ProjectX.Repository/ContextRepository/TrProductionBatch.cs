using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrProductionBatch
    {
        public int PbId { get; set; }
        public string? PbTitle { get; set; }
        public string? PbFileName { get; set; }
        public string? UId { get; set; }
        public DateTime? PbCreationDate { get; set; }
    }
}
