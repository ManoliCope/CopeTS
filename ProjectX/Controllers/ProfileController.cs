using ProjectX.Business.General;
using ProjectX.Business.Profile;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ProjectX.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IProfileBusiness _profileBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private User _user;

        private IWebHostEnvironment _env;
  

        public ProfileController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IProfileBusiness profileBusiness, IGeneralBusiness generalBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _profileBusiness = profileBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (User)httpContextAccessor.HttpContext.Items["User"];
            _env = env;

        }

        public IActionResult Index()
        {
            //Testupload();

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadCountries = true,
                loadProfileTypes = true,
                loadDocumentTypes = true
            });
            return View(response);
        }

        public IActionResult Create()
        {
            //Testupload();

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadCountries = true,
                loadProfileTypes = true,
                loadDocumentTypes = true
            });
            return View();
        }

        public IActionResult Contract()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadProducts = true,
                loadProfiles = true
            });
            return View(response);
        }

        [HttpPost]
        public SearchProfilesResp Search(SearchProfilesReq req)
        {
            return _profileBusiness.SearchProfiles(req);
        }



        public IActionResult ProfileView(GetProfileReq req)
        {

            LoadDataResp fill = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadProfileTypes = true,
                loadFeesTypes = true,
                loadCountries = true,
                loadCurrencies = true,
                loadDocumentTypes = true,
                loadTextReplacements = true,
                loadCaseComplexities = true,
                loadDepartments = true,
                loadProducts = true,
                loadAdditionalCoverage=true
            });
            ViewData["filldata"] = fill;

            if (req.id != 0)
            {
                var profile = GetProfile(req);
                profile.loadedData = fill.loadedData;
                if (profile.profile.HtmlGop != null)
                    profile.profile.HtmlGop = profile.profile.HtmlGop.Replace("</span>", "&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;<br><b><br>&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;&#9;</span>");

                return View(profile);
            }
            else
            {
                var emptyprofile = new GetProfileResp();
                emptyprofile.profile = new Entities.dbModels.Profile();
                emptyprofile.loadedData = new LoadDataModel();

                return View(emptyprofile);
            }
        }


        [HttpPost]

        public GetProfileResp GetProfile(GetProfileReq req)
        {
            req.withgop = true;
            return _profileBusiness.getProfile(req);
        }

        [HttpPost]
        public SaveProfileResp SaveProfile(string json)
        {
            SaveProfileReq req = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveProfileReq>(json);
            SaveProfileResp response = new SaveProfileResp();

            //ProfileFeesTypes profileFeesTypes = (ProfileFeesTypes)req.IdFeesType;

            //if (!Enum.IsDefined(typeof(ProfileFeesTypes), profileFeesTypes))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
            //    return response;
            //}

            if (string.IsNullOrEmpty(req.Name) || string.IsNullOrWhiteSpace(req.Name))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            if (req.profileTypes == null || req.profileTypes.Count <= 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileType);
                return response;
            }
            

            if (req.countries == null || req.countries.Count <= 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidCountry);
                return response;
            }

            //if (!ValueChecker.IsNullValue(req.Email) && !EmailManager.IsValidEmail(req.Email))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidEmail);
            //    return response;
            //}

            //if (!string.IsNullOrEmpty(req.Phone))
            //{
            //    PhoneNumberDetails profilePhone = PhoneNumberManager.CheckPhoneNumber(req.IntCode, req.Phone);

            //    if (!profilePhone.IsValid)
            //    {
            //        response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidPhoneNo);
            //        return response;
            //    }
            //    else
            //    {
            //        req.IntCode = profilePhone.CC;
            //        req.Phone = profilePhone.IntNum;
            //    }
            //}



            return _profileBusiness.saveProfile(req, _user.UserId);
        }

        [HttpPost]
        public GlobalResponse DeleteProfile(DeleteProfileReq req)
        {
            return _profileBusiness.deleteProfile(req, _user.UserId);
        }



        public GlobalResponse SaveGOP(SaveGopTemplateReq req)
        {
            return _profileBusiness.SaveGop(req);
        }



        private void EnsureDirectoryExists(string directory)
        {
            Directory.CreateDirectory(directory);
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private bool CheckFileExixts(string file)
        {
            return System.IO.File.Exists(file);
        }

        private string GetPathAndFilename(string filename, string brokerCode)
        {
            return @"D:\ccattachments\Profile\" + brokerCode + @"\" + filename;
        }

        // add producer file
        [HttpPost]
        public async Task<dynamic> addProducerFile(IList<IFormFile> files, string brokerCode)
        {
            try
            {
                //string directoryaaa = _env.WebRootPath + "\\dbserver\\ccattachments\\Profile\\" + brokerCode;

                string directory = @"D:\ccattachments\Profile\" + brokerCode;
                //string directory = _env.WebRootPath +  "\\dbserver\\ccattachments\\Profile\\" + brokerCode;
                this.EnsureDirectoryExists(directory);

                List<string> addedFiles = new List<string>();

                foreach (IFormFile source in files)
                {
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

                    filename = this.EnsureCorrectFilename(filename);

                    string fullPath = this.GetPathAndFilename(filename, brokerCode);

                    if (CheckFileExixts(fullPath))
                    {
                        dynamic results = new
                        {
                            files = "file exists - " + fullPath
                        };

                        return results;
                    }
                }

                foreach (IFormFile source in files)
                {
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

                    filename = this.EnsureCorrectFilename(filename);

                    string fullPath = this.GetPathAndFilename(filename, brokerCode);

                    using (FileStream output = System.IO.File.Create(fullPath))
                        await source.CopyToAsync(output);

                    addedFiles.Add(fullPath);
                }

                dynamic result = new
                {
                    files = addedFiles
                };
                return result;
            }
            catch (Exception e)
            {
                dynamic result = new
                {
                    files = e.Message.ToString()
                };

                return result;
                //return new BaseResponse(false, e.Message);
            }
        }


    }
}
