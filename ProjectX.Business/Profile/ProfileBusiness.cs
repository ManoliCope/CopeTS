using ProjectX.Business.Attachment;
using ProjectX.Entities;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
//using ProjectX.Entities.Models.Case;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using ProjectX.Entities.TableTypes;
using ProjectX.Repository.GeneralRepository;
using ProjectX.Repository.ProfileRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utilities;

namespace ProjectX.Business.Profile
{
    public class ProfileBusiness : IProfileBusiness
    {
        IProfileRepository _profileRepository;
        IGeneralRepository _generalRepository;
        IAttachmentBusiness _attachmentBusiness;

        public ProfileBusiness(IProfileRepository profileRepository, IGeneralRepository generalRepository, IAttachmentBusiness attachmentBusiness)
        {
            _profileRepository = profileRepository;
            _generalRepository = generalRepository;
            _attachmentBusiness = attachmentBusiness;
        }

        public SearchProfilesResp SearchProfiles(SearchProfilesReq req)
        {
            SearchProfilesResp response = new SearchProfilesResp();
            response.profiles = _profileRepository.getProfiles(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            return response;
        }
    
        public GetProfileResp getProfile(GetProfileReq req)
        {
            GetProfileResp response = new GetProfileResp();
            response.profile = _profileRepository.getProfile(req.id, req.withgop);
            if (response.profile != null)
                response.profile.attachmentModel = _attachmentBusiness.GetAttachments(FileDirectories.Profile_Documents, response.profile.IdProfile.ToString());

            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            return response;
        }

        public SaveProfileResp saveProfile(SaveProfileReq req, int IdUser)
        {
            SaveProfileResp response = new SaveProfileResp();
            int status = 0;
            response.profile = _profileRepository.saveProfile(req, IdUser, out status);
            if ((StatusCodeValues)status == StatusCodeValues.success)
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.IdProfile == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Profile");
            else
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, (StatusCodeValues)status);
            return response;
        }

        public GlobalResponse deleteProfile(DeleteProfileReq req, int IdUser)
        {
            GlobalResponse response = new GlobalResponse();
            _profileRepository.deleteProfile(req.Id, IdUser);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, SuccessCodeValues.Delete, "Profile");
            return response;
        }

     
        public GlobalResponse SaveGop(SaveGopTemplateReq req)
        {
            GlobalResponse response = new GlobalResponse();

            string[] charactersToReplace = new string[] { @"\n", @"\r" };

            //remove line breaks
            foreach (string s in charactersToReplace)
            {
                req.gopHtmlText = req.gopHtmlText.Replace(Regex.Unescape(s), string.Empty);
            }

            //multiple tab to one tab
            Regex.Replace(req.gopHtmlText, @"(\\t|\\n|&nbsp;)+", "");

            //set one tab to one space
            req.gopHtmlText = req.gopHtmlText.Replace(Regex.Unescape(@"\t"), " ");

            //set multiple white spaces into one
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);
            req.gopHtmlText = regex.Replace(req.gopHtmlText, " ");


            _profileRepository.SaveGop(req.idProfile, (int)DocumentTypes.GOP, req.gopHtmlText);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            return response;
        }


    }
}
