using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.ProductionBatch
{
    public class ProductionBatchGetResp : GlobalResponse
    {
        public dbModels.TR_ProductionBatch productionbatch { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
