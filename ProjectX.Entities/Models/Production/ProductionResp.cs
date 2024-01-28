using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Production

{
    public class ProductionResp : GlobalResponse
    {

        public string ValidationResp { get; set; }
        public List<QuotationResp> QuotationResp { get; set; }
        public List<TR_Benefit> AdditionalBenefits { get; set; }


    }
}
