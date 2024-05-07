using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrBeneficiary
    {
        public TrBeneficiary()
        {
            TrPolicyDetails = new HashSet<TrPolicyDetail>();
        }

        public int BeId { get; set; }
        public string? BeFirstName { get; set; }
        public string? BeMiddleName { get; set; }
        public string? BeLastName { get; set; }
        public string? BePassportNumber { get; set; }
        public DateTime? BeDob { get; set; }
        public int? BeSex { get; set; }
        public string? BeMaidenName { get; set; }
        public int? BeNationalityid { get; set; }
        public int? BeCountryResidenceid { get; set; }
        public DateTime? BeCreationDate { get; set; }
        public string? UId { get; set; }

        public virtual ICollection<TrPolicyDetail> TrPolicyDetails { get; set; }
    }
}
