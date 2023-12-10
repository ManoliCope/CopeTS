using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.ProductionBatch
{
    public class ProductionBatchSearchResp : GlobalResponse
    {
        public List<TR_ProductionBatch> productionbatch { get; set; }
    }

}
