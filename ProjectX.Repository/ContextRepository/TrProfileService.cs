using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrProfileService
    {
        public int PrId { get; set; }
        public byte PsIdProfileType { get; set; }
        public byte PsIdCaseType { get; set; }
        public bool? PsIsActive { get; set; }
        public DateTime PsCreated { get; set; }
    }
}
