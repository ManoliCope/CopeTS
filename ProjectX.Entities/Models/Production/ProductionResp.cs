using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Production

{
    public class ProductionResp : GlobalResponse
    {
        //production req
        public Models.Production.ProductionReq production { get; set; }
        public List<dbModels.TR_Tariff> tariff { get; set; }


    }
}
