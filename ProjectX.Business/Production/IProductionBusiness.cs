using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Product;
using ProjectX.Entities.Models.Production;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Production
{
    public interface IProductionBusiness
    {
        public int GetPolicyID(Guid id,int userid);
        public List<TR_Product> GetProductsByType(int id, int policyid, int userid);
        public List<TR_Zone> GetZonesByProduct(int id);
        public List<TR_Destinations> GetDestinationByZone(int id);
        public List<TR_Benefit> GetAdditionalBenbyTariff(List<int> Tariff);
        public ProductionResp getProductionDetails(List<ProductionReq> req,int userid);
        public ProductionSaveResp SaveIssuance(IssuanceReq req, int userid, TR_Users _user);
        public ProductionPolicy GetPolicy(int IdPolicy, int userid,bool isprint);
        public List<TR_PolicyHeader> GetPoliciesList(ProductionSearchReq req, int userid);
        public List<TR_Beneficiary> GetPolicyBeneficiaries(int id, int userid);
        public ProductionResp CancelProduction(int polId, int userid);
        public ProductionResp EditableProduction(int polId, int userid, bool isEditable);
        //public string printp(string html);
    }
}
