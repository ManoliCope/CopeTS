using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrLine
    {
        public int DiId { get; set; }
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}
