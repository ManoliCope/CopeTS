using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.ProductionBatch
{
    public class ProductionBatchSaveReq : GlobalResponse
    {
        public int userid { get; set; }
        public string title { get; set; }
        public List<ProductionBatchDetailsReq> productionbatches { get; set; }

    }

}
