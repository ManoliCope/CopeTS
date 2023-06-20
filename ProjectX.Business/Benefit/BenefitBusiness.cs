using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Benefit;
using ProjectX.Repository.BenefitRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Benefit
{
    public class BenefitBusiness : IBenefitBusiness
    {
        IBenefitRepository _benefitRepository;

        public BenefitBusiness(IBenefitRepository benefitRepository)
        {
            _benefitRepository = benefitRepository;
        }
        public BenResp ModifyBenefit(BenResp req)
        {
            return _benefitRepository.ModifyBenefit(req);
        }
        public List<TR_Benefit> GetBenefitList(BenReq req)
        {
            return _benefitRepository.GetBenefitList(req);
        }
        public TR_Benefit GetBenefit(int IdBenifit)
        {
            return _benefitRepository.GetBenefit(IdBenifit);
        }
    }
}
