using ProjectX.Entities;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Tariff;
using ProjectX.Entities.Resources;
using ProjectX.Repository.TariffRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Tariff
{
    public class TariffBusiness : ITariffBusiness
    {
        ITariffRepository _tariffRepository;

        public TariffBusiness(ITariffRepository tariffRepository)
        {
            _tariffRepository = tariffRepository;
        }
        public TariffResp ModifyTariff(TariffReq req, string act, int userid)
        {
            TariffResp response = new TariffResp();
            response = _tariffRepository.ModifyTariff(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Tariff");
            return response;
           
        }
        public List<TR_Tariff> GetTariffList(TariffSearchReq req)
        {
            return _tariffRepository.GetTariffList(req);
        }
        public TariffResp GetTariff(int IdTariff)
        {
            TR_Tariff repores = _tariffRepository.GetTariff(IdTariff);
            TariffResp resp = new TariffResp();
            resp.id = repores.T_Id;
            resp.idPackage = repores.P_Id;
            resp.start_age = repores.T_Start_Age;
            resp.end_age = repores.T_End_Age;
            resp.number_of_days = repores.T_Number_Of_Days;
            resp.price_amount = repores.T_Price_Amount;
            resp.net_premium_amount = repores.T_Net_Premium_Amount;
            resp.pa_amount = repores.T_PA_Amount;
            resp.tariff_starting_date = repores.T_Tariff_Starting_Date;
            resp.Override_Amount = repores.T_Override_Amount;
            resp.planId = repores.PL_Id;
            resp.package = repores.P_Name;
            resp.plan= repores.PL_Name;

            return resp;
           
        }
        public TariffResp Import(string filePath)
        {
            return _tariffRepository.Import(filePath);
        }
    }
}
