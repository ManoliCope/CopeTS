using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPolicyHeader
    {
        public TrPolicyHeader()
        {
            TrPolicyAdditionalBenefits = new HashSet<TrPolicyAdditionalBenefit>();
            TrPolicyDestinations = new HashSet<TrPolicyDestination>();
            TrPolicyDetails = new HashSet<TrPolicyDetail>();
        }

        public int PolicyId { get; set; }
        public string Reference { get; set; } = null!;
        public int? Duration { get; set; }
        public DateTime? Todate { get; set; }
        public DateTime? Fromdate { get; set; }
        public int? ProductId { get; set; }
        public int? ZoneId { get; set; }
        public bool? IsIndividual { get; set; }
        public bool? IsGroup { get; set; }
        public bool? IsFamily { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public bool? IsCanceled { get; set; }
        public decimal? InitialPremium { get; set; }
        public decimal? AdditionalValue { get; set; }
        public decimal? TaxVatvalue { get; set; }
        public decimal? StampsValue { get; set; }
        public decimal? GrandTotal { get; set; }
        public bool? IsEditable { get; set; }
        public int? Currency { get; set; }
        public string? Source { get; set; }
        public Guid PolicyGuid { get; set; }
        public DateTime? CanceledOn { get; set; }
        public bool? IsSent { get; set; }

        public virtual ICollection<TrPolicyAdditionalBenefit> TrPolicyAdditionalBenefits { get; set; }
        public virtual ICollection<TrPolicyDestination> TrPolicyDestinations { get; set; }
        public virtual ICollection<TrPolicyDetail> TrPolicyDetails { get; set; }
    }
}
