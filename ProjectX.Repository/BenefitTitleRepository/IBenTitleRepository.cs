using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.BenefitTitle;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.BenefitTitleRepository
{
    public interface IBenTitleRepository
    {
        public BenTitleResp ModifyBenTitle(BenTitleReq req, string act, int userid);

        public List<TR_BenefitTitle> GetBenTitleList(BenTitleSearchReq req);
        public TR_BenefitTitle GetBenTitle(int IdTitle);
    }
}
