using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Tariff;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.TariffRepository
{
    public interface ITariffRepository
    {
        public TariffResp ModifyTariff(TariffResp req);
        public List<TR_Tariff> GetTariffList(TariffReq req);
        public TR_Tariff GetTariff(int IdTariff);
    }
}
