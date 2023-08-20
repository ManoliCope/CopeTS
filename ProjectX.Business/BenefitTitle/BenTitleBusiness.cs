using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.BenefitTitle;
using ProjectX.Repository.BenefitTitleRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;

namespace ProjectX.Business.BenefitTitle
{
    public class BenTitleBusiness : IBenTitleBusiness
    {
        IBenTitleRepository _benTitleRepository;

        public BenTitleBusiness(IBenTitleRepository benTitleRepository)
        {
            _benTitleRepository = benTitleRepository;
        }
        public BenTitleResp ModifyBenTitle(BenTitleReq req, string act, int userid)
        {
            BenTitleResp response = new BenTitleResp();
            response = _benTitleRepository.ModifyBenTitle(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Benefit Title");
            return response;
           
        }
        public List<TR_BenefitTitle> GetBenTitleList(BenTitleSearchReq req)
        {
            return _benTitleRepository.GetBenTitleList(req);
        }
        public BenTitleResp GetBenTitle(int IdBenTitle)
        {
            TR_BenefitTitle repores = _benTitleRepository.GetBenTitle(IdBenTitle);
            BenTitleResp resp = new BenTitleResp();
            resp.id = repores.BT_Id;
            resp.title = repores.BT_Title;
           

            return resp;
        }
    }
}
