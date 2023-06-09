using ProjectX.Repository.BenefitRepository;
using ProjectX.Repository.COBRepository;
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
    }
}
