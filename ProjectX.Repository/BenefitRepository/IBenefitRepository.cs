using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Benefit;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.BenefitRepository
{
    public interface IBenefitRepository
    {
        public BenResp ModifyBenefit(BenReq req, string act, int userid);
        public List<TR_Benefit> GetBenefitList(BenSearchReq req);
        public TR_Benefit GetBenefit(int IdBenifit);
        public BenResp ImportDataBenefits(List<TR_Benefit> benefits, int userid);
    }
}
