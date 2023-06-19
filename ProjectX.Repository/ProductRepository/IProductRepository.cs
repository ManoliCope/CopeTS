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
        public ProdResp ModifyProduct(ProdReq req,string act,int userid);
        public List<TR_Product> GetProductlist(ProdSearchReq req);
        public TR_Product GetProduct(int Idproduct);

    }
}
