using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrProfile
    {
        public int PrId { get; set; }
        public string? PrName { get; set; }
        public string? PrDialCode { get; set; }
        public string? PrPhoneNumber { get; set; }
        public string? PrEmail { get; set; }
        public short? PrIdCurrency { get; set; }
        public byte? PrFeesType { get; set; }
        public decimal? PrSimpleCaseAmount { get; set; }
        public decimal? PrComplexCaseAmount { get; set; }
        public bool PrEmailNotification { get; set; }
        public bool PrApprovalRequired { get; set; }
        public string? PrAccountNo { get; set; }
        public DateTime PrCreated { get; set; }
        public int PrCreatedBy { get; set; }
        public bool PrIsDeleted { get; set; }
        public DateTime? PrDeletionDate { get; set; }
        public int? PrDeletedBy { get; set; }
    }
}
