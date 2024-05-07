using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrCurrency1
    {
        public int? IdCurrency { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = null!;
        public string? Symbol { get; set; }
        public byte FractionNo { get; set; }
        public bool IsActive { get; set; }
    }
}
