using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.BenefitTitle
{
    public class BenTitleGetResp : GlobalResponse
    {
        public dbModels.TR_BenefitTitle benefit_title { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
