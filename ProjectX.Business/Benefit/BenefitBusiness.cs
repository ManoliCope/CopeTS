using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Benefit;
using ProjectX.Repository.BenefitRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;
namespace ProjectX.Business.Benefit
{
    public class BenefitBusiness : IBenefitBusiness
    {
        IBenefitRepository _benefitRepository;

        public BenefitBusiness(IBenefitRepository benefitRepository)
        {
            _benefitRepository = benefitRepository;
        }
        public BenResp ModifyBenefit(BenReq req, string act, int userid) 
        {
            BenResp response = new BenResp();
            response = _benefitRepository.ModifyBenefit(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Benefit");
            return response;
           
        }
        public List<TR_Benefit> GetBenefitList(BenSearchReq req)
        {
            return _benefitRepository.GetBenefitList(req);
        }
        public BenResp GetBenefit(int IdBenifit)
        {
            TR_Benefit repores = _benefitRepository.GetBenefit(IdBenifit);
            BenResp resp = new BenResp();
            resp.id = repores.B_Id;
            resp.title = repores.B_Title;
            resp.limit= repores.B_Limit;
            resp.is_Plus= repores.B_Is_Plus;
            resp.packageId= repores.P_Id;
            resp.additionalBenefits= repores.B_Additional_Benefits;
            resp.additionalBenefitsFormat= repores.B_Additional_Benefits_Format;
            resp.titleId= repores.BT_Id;

            return resp;
        }
        public BenResp ImportDataBenefits(List<TR_Benefit> benefits, int userid)
        {
           return _benefitRepository.ImportDataBenefits(benefits, userid);
        }
    }
}
