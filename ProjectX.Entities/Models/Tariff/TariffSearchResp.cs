using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Tariff
{
    public class TariffSearchResp : GlobalResponse
    {
        public List<TR_Tariff> tariff { get; set; }
    }

}
