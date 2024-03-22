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


        public int GetPolicyID(Guid id, int userid)
        {
            return _prodRepository.GetPolicyID(id, userid);
        }
        public ProductionBusiness(IProductionRepository prodRepository)
        {
            _prodRepository = prodRepository;
        }
        public List<TR_Product> GetProductsByType(int type, int policyid, int userid)
        {
            return _prodRepository.GetProductsByType(type, policyid, userid);
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
            ProductionResp ProductionDetails = _prodRepository.getProductionDetails(req, userid);
            if (req.Count != ProductionDetails.QuotationResp.Where(x => x.RowId == 1).ToList().Count)
                ProductionDetails.QuotationResp = new List<QuotationResp>();

            return ProductionDetails;
        }
        public ProductionSaveResp SaveIssuance(IssuanceReq req, int userid, TR_Users _user)
        {
            ProductionSaveResp response = new ProductionSaveResp();

            if (req.policyId != 0)
            {
                ProductionPolicy prevPolicy = _prodRepository.GetPolicy(req.policyId, userid, false);
                decimal balance = req.GrandTotal - prevPolicy.GrandTotal;

                if (_user.U_Prepaid_Account == true && balance > (decimal)(_user.U_User_Balance ?? 0))
                {
                    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InsufficientFunds);
                    return response;
                }
            }
            else
            {
                if (_user.U_Prepaid_Account == true && req.GrandTotal > (decimal)(_user.U_User_Balance ?? 0))
                {
                    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InsufficientFunds);
                    return response;
                }
            }
        
            if (req.Is_Individual && req.beneficiaryDetails.Count > 1)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.NotIndividual);
                return response;
            }

            if (req.selectedDestinationIds != null && req.selectedDestinationIds.Any())
            {
                bool SameDestination = req.beneficiaryData.Any(beneficiary => req.selectedDestinationIds.Contains(beneficiary.countryResidenceid));

                if (SameDestination)
                {
                    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.CountryasDestination);
                    return response;
                }
            }


            if (req.Is_Individual && req.beneficiaryDetails.Count > 1)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.NotIndividual);
                return response;
            }

            var duplicateBeneficiary = req.beneficiaryData.Where(b => b.insuredId != 0).GroupBy(b => b.insuredId).FirstOrDefault(g => g.Count() > 1);
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
        public ProductionPolicy GetPolicy(int IdPolicy, int userid, bool isprint)
        {
            return _prodRepository.GetPolicy(IdPolicy, userid, isprint);
        }

        public List<TR_PolicyHeader> GetPoliciesList(ProductionSearchReq req, int userid)
        {
            return _prodRepository.GetPoliciesList(req, userid);
        }

        public List<TR_Beneficiary> GetPolicyBeneficiaries(int id, int userid)
        {
            return _prodRepository.GetPolicyBeneficiaries(id, userid);
        }
        public ProductionResp CancelProduction(int polId, int userid)
        {
            return _prodRepository.CancelProduction(polId, userid);
        }
        public ProductionResp EditableProduction(int polId, int userid, bool isEditable)
        {
            return _prodRepository.EditableProduction(polId, userid, isEditable);
        }

        //public string printp(string html)
        //{
        //    var test = _helperNonsql.ConvertHtmlToPDF(html);
        //    return test;
        //}
    }
}
