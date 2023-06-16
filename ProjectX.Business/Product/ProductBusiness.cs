using ProjectX.Repository.ProductRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Models.Product;
using ProjectX.Entities.dbModels;

namespace ProjectX.Business.Product
{
    public class ProductBusiness : IProductBusiness
    {
        IProductRepository _prodRepository;

        public ProductBusiness(IProductRepository prodRepository)
        {
            _prodRepository = prodRepository;
        }
        public ProdResp ModifyProduct(ProdResp req)
        {
            return _prodRepository.ModifyProduct(req);
        }
        public List<TR_Product> GetProduct(ProdReq req)
        {
            return _prodRepository.GetProduct(req);
        }
    }
}
