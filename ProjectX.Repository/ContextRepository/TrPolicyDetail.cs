using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPolicyDetail
    {
        public int PolicyDetailId { get; set; }
        public int PolicyId { get; set; }
        public string? Insured { get; set; }
        public string? FullName { get; set; }
        public string? Plan { get; set; }
        public bool? Deductible { get; set; }
        public bool? SportsActivities { get; set; }
        public decimal? DeductiblePrice { get; set; }
        public decimal? SportsActivitiesPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? PlanPrice { get; set; }
        public decimal? FinalPrice { get; set; }
        public int? InsuredId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? PassportNo { get; set; }
        public string? Gender { get; set; }
        public int? Tariff { get; set; }
        public decimal? NetPrice { get; set; }

        public virtual TrBeneficiary? InsuredNavigation { get; set; }
        public virtual TrPolicyHeader Policy { get; set; } = null!;
        public virtual TrTariff? TariffNavigation { get; set; }
    }
}
