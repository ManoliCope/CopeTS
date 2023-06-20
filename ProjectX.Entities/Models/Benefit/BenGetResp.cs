using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Benefit
{
    public class BenGetResp : GlobalResponse
    {
        public dbModels.TR_Benefit benefit { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
