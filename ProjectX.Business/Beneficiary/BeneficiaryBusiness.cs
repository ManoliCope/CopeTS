using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Beneficiary;
using ProjectX.Repository.BeneficiaryRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;

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
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Plan");
            return response;
           
        }
        public List<TR_Beneficiary> GetBeneficiaryList(BeneficiarySearchReq req)
        {
            return _beneficiaryRepository.GetBeneficiaryList(req);
        }
        public BeneficiaryResp GetBeneficiary(int IdBeneficiary)
        {
            TR_Beneficiary repores = _beneficiaryRepository.GetBeneficiary(IdBeneficiary);
            BeneficiaryResp resp = new BeneficiaryResp();
            resp.Id = repores.BE_Id;
            resp.Sex = repores.BE_Sex;
            resp.FirstName = repores.BE_FirstName;
            resp.MiddleName = repores.BE_MiddleName;
            resp.LastName = repores.BE_LastName;
            resp.DateOfBirth = repores.BE_DOB;
            resp.PassportNumber = repores.BE_PassportNumber;
           

            return resp;
        }
    }
}
