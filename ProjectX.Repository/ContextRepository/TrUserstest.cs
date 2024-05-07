using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrUserstest
    {
        public int UId { get; set; }
        public string? UFirstName { get; set; }
        public string? UMiddleName { get; set; }
        public string? ULastName { get; set; }
        public string? UCategory { get; set; }
        public long? UBrokerCode { get; set; }
        public int? UCountry { get; set; }
        public int? UCity { get; set; }
        public string? UTelephone { get; set; }
        public string? UEmail { get; set; }
        public int? USuperAgentId { get; set; }
        public string? UContactPerson { get; set; }
        public int? UInsuredNumber { get; set; }
        public double? UTax { get; set; }
        public int? UTaxType { get; set; }
        public int? UCurrency { get; set; }
        public int? URoundingRule { get; set; }
        public double? UUniqueTax { get; set; }
        public double? UUniqueAdminTax { get; set; }
        public double? UCommission { get; set; }
        public double? UStamp { get; set; }
        public double? UAdditionalFees { get; set; }
        public double? UVat { get; set; }
        public bool? UForSyria { get; set; }
        public bool? UShowCommission { get; set; }
        public bool? UFixedAdditionalFees { get; set; }
        public bool? UApplyRounding { get; set; }
        public bool? UAllowCancellation { get; set; }
        public bool? UShowCertificate { get; set; }
        public bool? UCancellationSubAgent { get; set; }
        public bool? UPreviewTotalOnly { get; set; }
        public bool? UPreviewNet { get; set; }
        public bool? UAgentsCreation { get; set; }
        public bool? UAgentsCreationApproval { get; set; }
        public bool? UAgentsCommissionReportView { get; set; }
        public bool? USubAgentsCommissionReportView { get; set; }
        public bool? UPrintClientVoucher { get; set; }
        public bool? UMultiLangPolicy { get; set; }
        public bool? UTaxInvoice { get; set; }
        public bool? UHidePremiumInfo { get; set; }
        public bool? UActive { get; set; }
        public string? UPassword { get; set; }
        public int? UCreatorId { get; set; }
        public int? UCountryCode { get; set; }
        public string? UUserName { get; set; }
        public bool? UIsAdmin { get; set; }
        public string? UCityName { get; set; }
        public double? UMaxAdditionalFees { get; set; }
        public DateTime? UCreationDate { get; set; }
        public double? URetention { get; set; }
        public string? ULogo { get; set; }
        public string? UPrintLayout { get; set; }
        public string? USignature { get; set; }
        public bool? UCanCancel { get; set; }
        public bool? UCanEdit { get; set; }
        public bool? UManualProduction { get; set; }
        public Guid UGuid { get; set; }
    }
}
