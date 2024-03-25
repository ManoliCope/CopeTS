using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
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
        public List<TR_Beneficiary> GetBeneficiaryList(BeneficiarySearchReq req, int userid);
        public BeneficiaryResp GetBeneficiary(int IdBeneficiary, int userid);
        public BeneficiarySearchResp SearchBeneficiaryPref(string prefix, int userid);
        public BeneficiariesBatchSaveResp SaveBeneficiariesBatch(BeneficiariesBatchSaveReq req);

    }
}
