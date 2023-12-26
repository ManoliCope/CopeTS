using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.ProductionBatch
{
    public class ProductionBatchSaveResp : GlobalResponse
    {

        //public int idProfile { get; set; }
        public int id { get; set; }
        public List<ProductionBatchDetailsResp> productionbatches { get; set; }


    }

}
