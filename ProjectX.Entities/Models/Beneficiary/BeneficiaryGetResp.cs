using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Beneficiary
{
    public class BeneficiaryGetResp : GlobalResponse
    {
        public dbModels.TR_Beneficiary beneficiary { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
