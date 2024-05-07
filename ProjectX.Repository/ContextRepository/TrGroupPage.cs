using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrGroupPage
    {
        public int GpId { get; set; }
        public int GrId { get; set; }
        public int PgId { get; set; }
        public bool? GpAllowInMenu { get; set; }
        public bool? GpAllowView { get; set; }
        public bool GpAllowInsert { get; set; }
        public bool GpAllowUpdate { get; set; }
        public bool GpAllowDelete { get; set; }
        public bool GpAllowPrint { get; set; }
        public bool GpAllowExecute { get; set; }
        public bool GpAllowCopy { get; set; }
        public bool GpAllowRenew { get; set; }
        public bool GpAllowAudit { get; set; }
        public bool GpIsDefault { get; set; }
    }
}
