using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.ProductRepository
{
    public interface IProductRepository
    {
        public ProdResp ModifyProduct(ProdResp req);
        public List<TR_Product> GetProduct(ProdReq req);
    }
}
