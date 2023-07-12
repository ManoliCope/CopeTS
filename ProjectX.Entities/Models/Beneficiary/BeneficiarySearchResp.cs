using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Beneficiary
{
    public class BeneficiarySearchResp : GlobalResponse
    {
        public List<TR_Beneficiary> beneficiary { get; set; }
    }

}
