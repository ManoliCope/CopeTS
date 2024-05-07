using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrFeedbackConfig
    {
        public int FeId { get; set; }
        public string FeDescription { get; set; } = null!;
        public bool FeIsNegative { get; set; }
        public int? FeIdService { get; set; }
        public bool? FeIsActive { get; set; }
        public DateTime FeCreated { get; set; }
    }
}
