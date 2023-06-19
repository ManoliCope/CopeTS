using ProjectX.Repository.ProductRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Models.Product;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Resources;
using ProjectX.Entities;

namespace ProjectX.Business.Product
{
    public class ProductBusiness : IProductBusiness
    {
        IProductRepository _prodRepository;

        public ProductBusiness(IProductRepository prodRepository)
        {
            _prodRepository = prodRepository;
        }
        public ProdResp ModifyProduct(ProdReq req, string act, int userid)
        {
            ProdResp response = new ProdResp();
            response = _prodRepository.ModifyProduct(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Product");
            return response;
        }
        public List<TR_Product> GetProductlist(ProdSearchReq req)
        {
            return _prodRepository.GetProductlist(req);
        }

        public ProdResp GetProduct(int Idproduct)
        {
            TR_Product repores = _prodRepository.GetProduct(Idproduct);
            ProdResp resp = new ProdResp();
            resp.id = repores.PR_Id;
            resp.title = repores.PR_Title;
            resp.description = repores.PR_Description;
            resp.is_family = repores.PR_Is_Family;
            resp.activation_date = repores.PR_Activation_Date;
            resp.is_active = repores.PR_Is_Active;
            resp.is_deductible = repores.PR_Is_Deductible;
            resp.sports_activities = repores.PR_Sports_Activities;
            resp.additional_benefits = repores.PR_Additional_Benefits;

            return resp;
        }
    }
}
