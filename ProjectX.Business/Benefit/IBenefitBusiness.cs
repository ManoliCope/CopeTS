using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Benefit;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Benefit
{
    public interface IBenefitBusiness
    {
        public BenResp ModifyBenefit(BenResp req);
        public List<TR_Benefit> GetBenefit(BenReq req);
    }
}
