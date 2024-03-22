using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Beneficiary
{
    public class BeneficiariesBatchSaveResp : GlobalResponse
    {
        public List<ImportBeneficiariesReq> beneficiaries { get; set; }

    }

}
