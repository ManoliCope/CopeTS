using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.BenefitTitle;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.BenefitTitle
{
    public interface IBenTitleBusiness
    {
        public BenTitleResp ModifyBenTitle(BenTitleReq req, string act, int userid);
        public List<TR_BenefitTitle> GetBenTitleList(BenTitleSearchReq req);
        public BenTitleResp GetBenTitle(int IdBenTitle);


    }
}
