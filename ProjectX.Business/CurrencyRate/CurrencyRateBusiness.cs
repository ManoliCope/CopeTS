using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.CurrencyRate;
using ProjectX.Repository.CurrencyRateRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;

namespace ProjectX.Business.CurrencyRate
{
    public class CurrencyRateBusiness : ICurrencyRateBusiness
    {
        ICurrencyRateRepository _currRepository;

        public CurrencyRateBusiness(ICurrencyRateRepository planRepository)
        {
            _currRepository = planRepository;
        }
        public CurrResp ModifyCurrencyRate(CurrReq req, string act, int userid)
        {
            CurrResp response = new CurrResp();
            response = _currRepository.ModifyCurrencyRate(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "CurrencyRate");
            return response;
           
        }
        public List<TR_CurrencyRate> GetCurrencyRateList(CurrSearchReq req)
        {
            return _currRepository.GetCurrencyRateList(req);
        }
        public CurrResp GetCurrencyRate(int IdCurr)
        {
            TR_CurrencyRate repores = _currRepository.GetCurrencyRate(IdCurr);
            CurrResp resp = new CurrResp();
            resp.Id = repores.CR_Id;
            resp.Currency_Id = repores.CR_Currency_Id;
            resp.Currency = repores.CR_Currency;
            resp.Rate = repores.CR_Rate;
            resp.Creation_Date = repores.CR_Creation_Date;
           

            return resp;
        }
    }
}
