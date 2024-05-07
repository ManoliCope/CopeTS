using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrGroupUser
    {
        public int GuId { get; set; }
        public int GrId { get; set; }
        public string? UsId { get; set; }
        public bool? GuIsActive { get; set; }
        public DateTime GuCreated { get; set; }
    }
}
