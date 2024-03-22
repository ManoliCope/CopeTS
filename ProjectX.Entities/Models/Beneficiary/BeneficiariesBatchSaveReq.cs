using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Beneficiary
{
    public class BeneficiariesBatchSaveReq : GlobalResponse
    {
        public int userid { get; set; }
        public List<ImportBeneficiariesReq> beneficiaries { get; set; }

    }

}
