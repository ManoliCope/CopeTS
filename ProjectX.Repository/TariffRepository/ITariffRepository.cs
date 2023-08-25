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
        public TariffResp ModifyTariff(TariffReq req, string act, int userid);
        public List<TR_Tariff> GetTariffList(TariffSearchReq req);
        public TR_Tariff GetTariff(int IdTariff);
        public TariffResp ImportDataTariff(List<TR_Tariff> tariffs, int userid);
    }
}
