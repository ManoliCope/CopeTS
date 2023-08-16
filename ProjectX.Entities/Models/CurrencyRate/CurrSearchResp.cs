using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.CurrencyRate
{
    public class CurrSearchResp : GlobalResponse
    {
        public List<TR_CurrencyRate> currencyRate { get; set; }
    }

}
