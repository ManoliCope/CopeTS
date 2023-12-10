using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.ProductionBatch;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.ProductionBatchRepository
{
    public interface IProductionBatchRepository
    {
        //public ZoneResp ModifyZone(ZoneReq req, string act, int userid);
        public List<TR_ProductionBatch> GetProductionBatchList(ProductionBatchSearchReq req);
        public TR_ProductionBatch GetProductionBatch(int batchid);
    }
}
