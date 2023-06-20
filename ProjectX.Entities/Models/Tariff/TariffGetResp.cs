using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Tariff
{
    public class TariffGetResp : GlobalResponse
    {
        public dbModels.TR_Tariff tariff { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
