using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.CurrencyRate
{
    public class CurrGetResp : GlobalResponse
    {
        public dbModels.TR_CurrencyRate currencyRate { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
