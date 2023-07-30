﻿using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Product;
using ProjectX.Entities.Models.Production;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.ProductionRepository
{
    public interface IProductionRepository
    {
        public List<TR_Product> GetProductsByType(int id);
        public List<TR_Zone> GetZonesByProduct(int id);
        public List<TR_Destinations> GetDestinationByZone(int id);
        public List<ProductionResp> getProductionDetails(List<ProductionReq> req);


    }
}
