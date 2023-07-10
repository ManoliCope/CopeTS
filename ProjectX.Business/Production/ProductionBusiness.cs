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

namespace ProjectX.Business.Production
{
    public class ProductionBusiness : IProductionBusiness
    {
        IProductionRepository _prodRepository;

        public ProductionBusiness(IProductionRepository prodRepository)
        {
            _prodRepository = prodRepository;
        }
        public List<TR_Product> GetProductsByType(int type)
        {
            return _prodRepository.GetProductsByType(type);
        }
        public List<TR_Destinations> GetDestinationByZone(int idZone)
        {
            return _prodRepository.GetDestinationByZone(idZone);
        }
    }
}
