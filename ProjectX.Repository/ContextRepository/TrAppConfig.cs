using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrAppConfig
    {
        public short AcId { get; set; }
        public string AcCode { get; set; } = null!;
        public string AcName { get; set; } = null!;
        public string AcValue { get; set; } = null!;
        public bool AcIsActive { get; set; }
    }
}
