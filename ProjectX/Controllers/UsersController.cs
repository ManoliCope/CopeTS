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
using ProjectX.Business.General;
using ProjectX.Entities.Models.Users;
using ProjectX.Business.Users;

namespace ProjectX.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUsersBusiness _usersBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private User _user;

        private IWebHostEnvironment _env;
  

        public UsersController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            //_profileBusiness = profileBusiness;
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
            return null;
            //return _profileBusiness.SearchProfiles(req);
        }

       public IActionResult ResetPass()
        {
            //LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            //{
            //    loadProducts = true,
            //    loadProfiles = true
            //});
            return View();
        }
        [HttpPost]
        public ResetPass resetPassword(ResetPass res)
        {
                res.userId = _user.UserId;
                var resp= _usersBusiness.resetPass(res);
                return resp;
        }
    }
}
