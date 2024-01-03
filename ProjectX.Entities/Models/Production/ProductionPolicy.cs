using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.CurrencyRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.Models.Production
{
    public class ProductionPolicy
    {
        public int PolicyID { get; set; }
        public string Reference { get; set; }
        public int Duration { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ZoneID { get; set; }
        public string ZoneName { get; set; }
        public bool IsIndividual { get; set; }
        public bool IsFamily { get; set; }
        public bool IsGroup { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsEditable { get; set; }
        public decimal InitialPremium { get; set; }
        public decimal AdditionalValue { get; set; }
        public decimal TaxVATValue { get; set; }
        public decimal StampsValue { get; set; }
        public decimal GrandTotal { get; set; }
        public int Status { get; set; }

        public List<PolicyDetail> PolicyDetails { get; set; }
        public List<PolicyAdditionalBenefit> AdditionalBenefits { get; set; }
        public List<PolicyDestination> Destinations { get; set; }
        public List<TR_Benefit> Benefits { get; set; }
        public CurrResp CurrencyRate { get; set; }
        public string QrCodebit { get; set; }
        public string Layout { get; set; }
        public string Signature { get; set; }
        public int prttyp { get; set; }
    }

    public class PolicyDetail
    {
        public int PolicyDetailID { get; set; }
        public int PolicyID { get; set; }
        public int Insured { get; set; }
        public string FullName { get; set; }
        public int Plan { get; set; }
        public string PlanName { get; set; }

        public bool Deductible { get; set; }
        public decimal DeductiblePrice { get; set; }
        public bool SportsActivities { get; set; }
        public decimal SportsActivitiesPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal PlanPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public int InsuredId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string PassportNo { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string CountryResidence { get; set; }
        public int Tariff { get; set; }
        public decimal TariffPrice { get; set; }
    }

    public class PolicyAdditionalBenefit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PolicyID { get; set; }
        public int AB_ID { get; set; }
        public int Insuredid { get; set; }
        public decimal Price { get; set; }
    }

    public class PolicyDestination
    {
        public int ID { get; set; }
        public int PolicyID { get; set; }
        public int DestinationID { get; set; }
        public string DestinationName { get; set; }
    }
}
