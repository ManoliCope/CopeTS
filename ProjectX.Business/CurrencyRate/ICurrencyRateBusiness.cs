using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.CurrencyRate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.CurrencyRate
{
    public interface ICurrencyRateBusiness
    {
        public CurrResp ModifyCurrencyRate(CurrReq req, string act, int userid);
        public List<TR_CurrencyRate> GetCurrencyRateList(CurrSearchReq req);
        public CurrResp GetCurrencyRate(int IdPlan);
    }
}
