using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrBenefitTitle
    {
        public TrBenefitTitle()
        {
            TrBenefits = new HashSet<TrBenefit>();
        }

        public int BtId { get; set; }
        public string? BtTitle { get; set; }

        public virtual ICollection<TrBenefit> TrBenefits { get; set; }
    }
}
