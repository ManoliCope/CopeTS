using ProjectX.Entities.dbModels;
//using ProjectX.Entities.Models.Case;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.TableTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.ProfileRepository
{
    public interface IProfileRepository
    {
        List<Profile> getProfiles(SearchProfilesReq req);
        List<ProfileContract> getContracts(SearchProfilesReq req);
        Profile getProfile(int IdProfile, bool withGOP);
        Profile saveProfile(SaveProfileReq req, int IdUser, out int status);
        void deleteProfile(int IdProfile, int IdUser);
        //void SaveAdherent(int IdProfile, int IdProduct, string from, string to,List<TR_Adherent> adherents);
        void SaveGop(int IdProfile, int IdDocumentType, string htmlText);
        //List<Contact> getContactEmails(SaveCaseReq req);
    }
}
