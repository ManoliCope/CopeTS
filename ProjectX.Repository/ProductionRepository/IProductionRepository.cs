using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Product;
using ProjectX.Entities.Models.Production;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.ProductionRepository
{
    public interface IProductionRepository
    {
        public List<TR_PolicyHeader> GetPoliciesList(ProductionSearchReq req, int userid);
        public List<TR_Product> GetProductsByType(int id, int userId);
        public List<TR_Zone> GetZonesByProduct(int id);
        public List<TR_Destinations> GetDestinationByZone(int id);
        public List<TR_Benefit> GetAdditionalBenbyTariff(List<int> Tariff);
        public ProductionResp getProductionDetails(List<ProductionReq> req, int userid);
        public ProductionSaveResp SaveIssuance(IssuanceReq req, int userid);
        public ProductionPolicy GetPolicy(int IdPolicy, int userid);
        public List<TR_Beneficiary> GetPolicyBeneficiaries(int IdPolicy, int userid);
        
    }
}
