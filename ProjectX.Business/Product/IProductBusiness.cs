using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Product
{
    public interface IProductBusiness
    {
        public ProdResp ModifyProduct(ProdReq req, string act,int userid);
        public List<TR_Product> GetProductlist(ProdSearchReq req);
        public ProdResp GetProduct(int Idproduct);

    }
}
