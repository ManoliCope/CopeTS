using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Plan
{
    public class PlanGetResp : GlobalResponse
    {
        public dbModels.TR_Plan plan { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
