using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Zone
{
    public class ZoneGetResp : GlobalResponse
    {
        public dbModels.TR_Zone zone { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
