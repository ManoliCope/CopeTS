using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrTextReplacement
    {
        public string TrCode { get; set; } = null!;
        public string? TrDesc { get; set; }
        public bool? TrIsActive { get; set; }
    }
}
