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

namespace ProjectX.Repository.ProductionBatch
{
    public class ProductionBatchBusiness : IProductionBatchBusiness
    {
        IProductionBatchRepository _productionBatchRepository;

        public ProductionBatchBusiness(IProductionBatchRepository productionBatchRepository)
        {
            _productionBatchRepository = productionBatchRepository;
        }

        public List<TR_ProductionBatch> GetProductionBatchList(ProductionBatchSearchReq req)
        {
           
            return _productionBatchRepository.GetProductionBatchList(req);
        }
        public TR_ProductionBatch GetProductionBatch(int batchid)
        {
            
            return _productionBatchRepository.GetProductionBatch(batchid);
        }
        //SaveProductionBatch
        public ProductionBatchSaveResp SaveProductionBatch(ProductionBatchSaveReq req)
        {
           // ProductionBatchSaveReq response = new ProductionBatchSaveReq();

            //List<TR_P> _ProductionBatches = new List<TR_Production>();

            //if (req.productionbatches != null && req.productionbatches.Count > 0)
            //{
            //    foreach (TR_ProductionBatch production in req.productionbatches)
            //    {
            //        _ProductionBatches.Add(new TR_ProductionBatch
            //        {
            //            IdAdherent = production.PB_Id,
            //            //DOB = adherent.DOB,
            //            FirstName = production.FirstName,
            //            LastName = production.LastName,
            //            Email = production.Email,
            //            ExpiryDate = production.ExpiryDate,
            //            InceptionDate = production.InceptionDate,
            //            IdInsured = production.IdInsured,
            //            IdPolicy = production.IdPolicy,
            //            Mobile = production.Mobile,
            //            Passport = production.Passport,
            //            PolicyNo = production.PolicyNo
            //        });
            //    }
            //}

            //_profileRepository.SaveAdherent(req.idProfile, req.idProduct, req.fromdate, req.todate, _Adherents);
            //response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            //return response;
            return _productionBatchRepository.SaveProductionBatch(req);
        }
    }
}
