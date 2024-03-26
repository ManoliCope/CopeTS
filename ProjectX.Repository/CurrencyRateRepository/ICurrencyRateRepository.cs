using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.CurrencyRate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.CurrencyRateRepository
{
    public interface ICurrencyRateRepository
    {
        public CurrResp ModifyCurrencyRate(CurrReq req, string act, int userid);
        public List<TR_CurrencyRate> GetCurrencyRateList(CurrSearchReq req);
        public List<TR_CurrencyRate> GetCurrencyRateListbyUserid(int Userid);
        public TR_CurrencyRate GetCurrencyRate(int IdCurrencyRate);
    }
}
