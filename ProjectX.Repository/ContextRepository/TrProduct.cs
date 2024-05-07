using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrProduct
    {
        public TrProduct()
        {
            TrPackages = new HashSet<TrPackage>();
        }

        public int PrId { get; set; }
        public string? PrTitle { get; set; }
        public string? PrDescription { get; set; }
        public bool? PrIsFamily { get; set; }
        public DateTime? PrActivationDate { get; set; }
        public bool? PrIsActive { get; set; }
        public double? PrIsDeductible { get; set; }
        public double? PrSportsActivities { get; set; }
        public double? PrAdditionalBenefits { get; set; }
        public bool? PrIsIndividual { get; set; }
        public bool? PrIsGroup { get; set; }
        public int? PrDeductibleFormat { get; set; }
        public int? PrSportsActivityFormat { get; set; }
        public int? PrAdditionalBenefitsFormat { get; set; }

        public virtual ICollection<TrPackage> TrPackages { get; set; }
    }
}
