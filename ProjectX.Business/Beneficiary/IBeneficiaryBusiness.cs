using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Beneficiary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Beneficiary
{
    public interface IBeneficiaryBusiness
    {
        public BeneficiaryResp ModifyBeneficiary(BeneficiaryReq req, string act, int userid);
        public List<TR_Beneficiary> GetBeneficiaryList(BeneficiarySearchReq req);
        public BeneficiaryResp GetBeneficiary(int IdBeneficiary);
        public BeneficiarySearchResp SearchBeneficiaryPref(string prefix);

    }
}
