using ProjectX.Entities;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Beneficiary;
using ProjectX.Entities.Resources;
using ProjectX.Repository.BeneficiaryRepository;

namespace ProjectX.Business.Beneficiary
{
    public class BeneficiaryBusiness : IBeneficiaryBusiness
    {
        IBeneficiaryRepository _beneficiaryRepository;

        public BeneficiaryBusiness(IBeneficiaryRepository beneficiaryRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
        }
        public BeneficiaryResp ModifyBeneficiary(BeneficiaryReq req, string act, int userid)
        {
            BeneficiaryResp response = new BeneficiaryResp();
            response = _beneficiaryRepository.ModifyBeneficiary(req, act, userid);
            if (act == "Delete")
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Delete, "Beneficiary");
            else
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Beneficiary");
            return response;

        }
        public List<TR_Beneficiary> GetBeneficiaryList(BeneficiarySearchReq req, int userid)
        {
            return _beneficiaryRepository.GetBeneficiaryList(req, userid);
        }
        public BeneficiaryResp GetBeneficiary(int IdBeneficiary, int userid)
        {
            TR_Beneficiary repores = _beneficiaryRepository.GetBeneficiary(IdBeneficiary, userid);
            BeneficiaryResp resp = new BeneficiaryResp();
            if (repores == null)
                return resp;
            else
            {
                resp.Id = repores.BE_Id;
                resp.Sex = repores.BE_Sex;
                resp.FirstName = repores.BE_FirstName;
                resp.MiddleName = repores.BE_MiddleName;
                resp.LastName = repores.BE_LastName;
                resp.MaidenName = repores.BE_MaidenName;
                resp.DateOfBirth = repores.BE_DOB;
                resp.PassportNumber = repores.BE_PassportNumber;
                resp.CountryResidenceid = repores.BE_CountryResidenceid;
                resp.Nationalityid = repores.BE_Nationalityid;
            }


            return resp;
        }
        public BeneficiarySearchResp SearchBeneficiaryPref(string prefix, int userid)
        {
            return _beneficiaryRepository.SearchBeneficiaryPref(prefix, userid);
        }
        public BeneficiariesBatchSaveResp SaveBeneficiariesBatch(BeneficiariesBatchSaveReq req)
        {
            return _beneficiaryRepository.SaveBeneficiariesBatch(req);
        }
    }
}
