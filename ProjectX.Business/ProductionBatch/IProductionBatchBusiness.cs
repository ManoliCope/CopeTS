using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.ProductionBatch;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.ProductionBatch
{
    public interface IProductionBatchBusiness
    {
        //public ZoneResp ModifyZone(ZoneReq req, string act, int userid);
        public List<TR_ProductionBatch> GetProductionBatchList(ProductionBatchSearchReq req,int userid);
        public TR_ProductionBatch GetProductionBatch(int batchid);
        public ProductionBatchSaveResp SaveProductionBatch(ProductionBatchSaveReq req);
    }
}
