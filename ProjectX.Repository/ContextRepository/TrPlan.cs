using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPlan
    {
        public TrPlan()
        {
            TrTariffs = new HashSet<TrTariff>();
        }

        public int PlId { get; set; }
        public string? PlTitle { get; set; }
        public bool? PlPaIncluded { get; set; }

        public virtual ICollection<TrTariff> TrTariffs { get; set; }
    }
}
