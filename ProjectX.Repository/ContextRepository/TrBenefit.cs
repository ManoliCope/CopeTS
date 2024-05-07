using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrBenefit
    {
        public int BId { get; set; }
        public string? BTitle { get; set; }
        public string? BLimit { get; set; }
        public int? PId { get; set; }
        public bool? BIsPlus { get; set; }
        public double? BAdditionalBenefits { get; set; }
        public int? BAdditionalBenefitsFormat { get; set; }
        public int? BtId { get; set; }
        public DateTime? BCreationDate { get; set; }

        public virtual TrBenefitTitle? Bt { get; set; }
        public virtual TrPackage? PIdNavigation { get; set; }
    }
}
