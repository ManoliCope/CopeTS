using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPackage
    {
        public TrPackage()
        {
            TrBenefits = new HashSet<TrBenefit>();
            TrTariffs = new HashSet<TrTariff>();
        }

        public int PId { get; set; }
        public string? PName { get; set; }
        public int? PrId { get; set; }
        public int? PlId { get; set; }
        public int? PZoneId { get; set; }
        public int? PRemoveDeductable { get; set; }
        public int? PAdultNo { get; set; }
        public int? PChildrenNo { get; set; }
        public bool? PPaIncluded { get; set; }
        public int? PAdultMaxAge { get; set; }
        public int? PChildMaxAge { get; set; }
        public bool? PSpecialCase { get; set; }
        public int? PRemoveSportsActivities { get; set; }

        public virtual TrZone? PZone { get; set; }
        public virtual TrProduct? Pr { get; set; }
        public virtual ICollection<TrBenefit> TrBenefits { get; set; }
        public virtual ICollection<TrTariff> TrTariffs { get; set; }
    }
}
