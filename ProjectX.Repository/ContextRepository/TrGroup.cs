using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrGroup
    {
        public int GrId { get; set; }
        public string GrName { get; set; } = null!;
        public bool GrIsAdmin { get; set; }
        public bool? GrIsActive { get; set; }
        public DateTime GrCreated { get; set; }
    }
}
