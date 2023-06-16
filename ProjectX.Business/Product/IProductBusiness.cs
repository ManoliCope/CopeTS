using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Product
{
    public interface IProductBusiness
    {
        public ProdResp ModifyProduct(ProdResp req);
        public List<TR_Product> GetProduct(ProdReq req);
    }
}
