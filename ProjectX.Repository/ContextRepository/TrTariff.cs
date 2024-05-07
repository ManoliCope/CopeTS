using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrTariff
    {
        public TrTariff()
        {
            TrPolicyDetails = new HashSet<TrPolicyDetail>();
        }

        public int TId { get; set; }
        public int? PId { get; set; }
        public int? TStartAge { get; set; }
        public int? TEndAge { get; set; }
        public int? TNumberOfDays { get; set; }
        public decimal? TPriceAmount { get; set; }
        public decimal? TNetPremiumAmount { get; set; }
        public decimal? TPaAmount { get; set; }
        public DateTime? TTariffStartingDate { get; set; }
        public decimal? TOverrideAmount { get; set; }
        public int? PlId { get; set; }

        public virtual TrPackage? PIdNavigation { get; set; }
        public virtual TrPlan? Pl { get; set; }
        public virtual ICollection<TrPolicyDetail> TrPolicyDetails { get; set; }
    }
}
