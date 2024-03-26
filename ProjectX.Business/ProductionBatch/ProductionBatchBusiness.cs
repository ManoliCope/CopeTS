using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using Utilities;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.ProductionBatch;
using ProjectX.Repository.ProductionBatchRepository;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;

namespace ProjectX.Repository.ProductionBatch
{
    public class ProductionBatchBusiness : IProductionBatchBusiness
    {
        IProductionBatchRepository _productionBatchRepository;

        public ProductionBatchBusiness(IProductionBatchRepository productionBatchRepository)
        {
            _productionBatchRepository = productionBatchRepository;
        }

        public List<TR_ProductionBatch> GetProductionBatchList(ProductionBatchSearchReq req,int userid)
        {
           
            return _productionBatchRepository.GetProductionBatchList(req, userid);
        }
        public TR_ProductionBatch GetProductionBatch(int batchid)
        {
            
            return _productionBatchRepository.GetProductionBatch(batchid);
        }
        //SaveProductionBatch
        public ProductionBatchSaveResp SaveProductionBatch(ProductionBatchSaveReq req)
        {
            return _productionBatchRepository.SaveProductionBatch(req);
        }
    }
}
