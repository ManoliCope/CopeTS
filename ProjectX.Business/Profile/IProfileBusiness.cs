using ProjectX.Entities;
using ProjectX.Entities.dbModels;
//using ProjectX.Entities.Models.Case;
using ProjectX.Entities.Models.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Profile
{
    public interface IProfileBusiness
    {
        SearchProfilesResp SearchProfiles(SearchProfilesReq req);
        //SearchContractResp SearchContracts(SearchProfilesReq req);
        GetProfileResp getProfile(GetProfileReq req);
        SaveProfileResp saveProfile(SaveProfileReq req, int IdUser);
        GlobalResponse deleteProfile(DeleteProfileReq req, int IdUser);
        //SaveAdherentResp SaveAdherents(SaveAdherentReq req);
        GlobalResponse SaveGop(SaveGopTemplateReq req);
        //List<Contact> GetContactEmails(SaveCaseReq req);
    }
}
