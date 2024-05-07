using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository 
{ 
    public partial class InsuredQuotation 
    {
        public int Id { get; set; }
        public int? Ages { get; set; }
        public int? Product { get; set; }
        public int? Zone { get; set; }
        public int? Durations { get; set; }
    }
}
