using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Production
{
    //public class ProductionReq
    //{
    //    public List<insuredQuotation> InsuredQuotations { get; set; }
    //}
    public class IssuanceReq
    {
        public List<BeneficiaryDetails> beneficiaryDetails { get; set; }
        public List<BeneficiaryData> beneficiaryData { get; set; }
        public List<AdditionalBenefit> additionalBenefits { get; set; }
        public List<int> selectedDestinationIds { get; set; }
        public string selectedDestinations { get; set; }
        public string duration { get; set; }
        public string to { get; set; }
        public string from { get; set; }

        public bool is_family { get; set; }
        public bool Is_Individual { get; set; }
        public bool Is_Group { get; set; }
        public string productId { get; set; }
        public string zoneId { get; set; }
        public decimal InitialPremium { get; set; }
        public decimal AdditionalValue { get; set; }
        public decimal TaxVATValue { get; set; }
        public decimal StampsValue { get; set; }
        public decimal GrandTotal { get; set; }

    }

    public class AdditionalBenefit
    {
        public int insuredId { get; set; }
        public string value { get; set; }
        public double price { get; set; }
    }
    public class BeneficiaryDetails
    {
        public int Insured { get; set; }
        public int tariff { get; set; }
        public string fullName { get; set; }
        public string plan { get; set; }
        public double Deductibleprice { get; set; }
        public double SportsActivitiesprice { get; set; }
        public double discount { get; set; }
        public double planPrice { get; set; }
        public double finalPrice { get; set; }
    }

    public class BeneficiaryData
    {
        public int Insured { get; set; }
        public int insuredId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public int age { get; set; }
        public string passportNo { get; set; }
        public string gender { get; set; }
    }

   

}
