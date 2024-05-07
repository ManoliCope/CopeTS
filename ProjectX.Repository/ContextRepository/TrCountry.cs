using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrCountry
    {
        public int CtId { get; set; }
        public string? CtCode2 { get; set; }
        public string? CtCode3 { get; set; }
        public string CtName { get; set; } = null!;
        public string? CtIntDialCode { get; set; }
        public string? CtM49 { get; set; }
        public string? CtFifa { get; set; }
        public string? CtContinent { get; set; }
        public bool? CtIsActive { get; set; }
    }
}
