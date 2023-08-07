using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Production

{
    public class QuotationResp 
    {
        public int RowId { get; set; }
        public int Insured { get; set; }
        public string Fullname { get; set; }
        public int Age { get; set; }
        public int Product { get; set; }
        public int Package{ get; set; }
        public string ProductName { get; set; }
        public int Zone { get; set; }
        public string ZoneName { get; set; }
        public int Plan { get; set; }
        public string PlanName { get; set; }
        public int Duration { get; set; }
        public int TariffID { get; set; }
        public int NumberOfDays { get; set; }
        public decimal PriceAmount { get; set; }
        public decimal NetPremiumAmount { get; set; }
        public decimal PAAmount { get; set; }
        public DateTime TariffStartingDate { get; set; }
        public decimal OverrideAmount { get; set; }
        public decimal DeductibleFee { get; set; }
        public decimal SportAcitiviesFee { get; set; }


    }
}
