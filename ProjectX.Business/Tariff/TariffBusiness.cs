using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Tariff;
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
        public TariffResp ModifyTariff(TariffResp req)
        {
            return _tariffRepository.ModifyTariff(req);
        }
        public List<TR_Tariff> GetTariffList(TariffReq req)
        {
            return _tariffRepository.GetTariffList(req);
        }
        public TR_Tariff GetTariff(int IdTariff)
        {
            return _tariffRepository.GetTariff(IdTariff);
        }
    }
}
