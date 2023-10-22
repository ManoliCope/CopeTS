using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Beneficiary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.BeneficiaryRepository
{
    public interface IBeneficiaryRepository
    {
        public BeneficiaryResp ModifyBeneficiary(BeneficiaryReq req, string act, int userid);
        public List<TR_Beneficiary> GetBeneficiaryList(BeneficiarySearchReq req, int userid);
        public TR_Beneficiary GetBeneficiary(int IdBeneficiary, int userid); //test
        public BeneficiarySearchResp SearchBeneficiaryPref(string prefix, int userid);
    }
}
