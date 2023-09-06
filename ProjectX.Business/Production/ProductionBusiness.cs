using ProjectX.Repository.ProductRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Models.Product;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Resources;
using ProjectX.Entities;
using ProjectX.Entities.bModels;
using ProjectX.Repository.ProductionRepository;
using ProjectX.Entities.Models.Production;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;

namespace ProjectX.Business.Production
{
    public class ProductionBusiness : IProductionBusiness
    {
        IProductionRepository _prodRepository;

        public ProductionBusiness(IProductionRepository prodRepository)
        {
            _prodRepository = prodRepository;
        }
        public List<TR_Product> GetProductsByType(int type, int userId)
        {
            return _prodRepository.GetProductsByType(type, userId);
        }
        public List<TR_Zone> GetZonesByProduct(int type)
        {
            return _prodRepository.GetZonesByProduct(type);
        }
        public List<TR_Destinations> GetDestinationByZone(int idZone)
        {
            return _prodRepository.GetDestinationByZone(idZone);
        }
        public List<TR_Benefit> GetAdditionalBenbyTariff(List<int> Tariff)
        {
            return _prodRepository.GetAdditionalBenbyTariff(Tariff);
        }
        public ProductionResp getProductionDetails(List<ProductionReq> req, int userid)
        {
            return _prodRepository.getProductionDetails(req, userid);
        }
        public ProductionSaveResp SaveIssuance(IssuanceReq req, int userid)
        {
            ProductionSaveResp response = new ProductionSaveResp();
            var duplicateBeneficiary = req.beneficiaryData.GroupBy(b => b.insuredId).FirstOrDefault(g => g.Count() > 1);
            if (duplicateBeneficiary != null)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.DuplicateBeneficiary);
                return response;
            }

            if (req.GrandTotal < 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.NegativeValues);
                return response;
            }

            if (req.Is_Individual && req.beneficiaryData.Count() > 1)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.IndividualMax);
                return response;
            }

            return _prodRepository.SaveIssuance(req, userid);
        }
        public ProductionPolicy GetPolicy(int IdPolicy, int userid)
        {
            return _prodRepository.GetPolicy(IdPolicy, userid);
        }

        public List<TR_PolicyHeader> GetPoliciesList(ProductionSearchReq req, int userid)
        {
            return _prodRepository.GetPoliciesList(req, userid);
        }

        public List<TR_Beneficiary> GetPolicyBeneficiaries(int id, int userid)
        {
            return _prodRepository.GetPolicyBeneficiaries(id, userid);
        }


        //public string printp(string html)
        //{
        //    var test = _helperNonsql.ConvertHtmlToPDF(html);
        //    return test;
        //}
    }
}
