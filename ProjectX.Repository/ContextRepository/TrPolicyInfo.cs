using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPolicyInfo
    {
        public int PiId { get; set; }
        public int PolicyId { get; set; }
        public int UId { get; set; }
        public int? UpId { get; set; }
        public double? UTax { get; set; }
        public int? UTaxType { get; set; }
        public int? URoundingRule { get; set; }
        public double? UCommission { get; set; }
        public double? UStamp { get; set; }
        public double? UVat { get; set; }
        public double? UMaxAdditionalFees { get; set; }
        public double? URetention { get; set; }
    }
}
