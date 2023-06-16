using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Tariff;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Tariff
{
    public interface ITariffBusiness
    {
        public TariffResp ModifyTariff(TariffResp req);
        public List<TR_Tariff> GetTariff(TariffReq req);
    }
}
