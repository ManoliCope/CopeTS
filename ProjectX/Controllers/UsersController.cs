﻿using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Business.Jwt;
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
        private TR_Users _user;
        private IJwtBusiness _jwtBusiness;

        private IWebHostEnvironment _env;
  

        public UsersController(IHttpContextAccessor httpContextAccessor, 
            IOptions<TrAppSettings> appIdentitySettingsAccessor, 
            IGeneralBusiness generalBusiness, IJwtBusiness jwtBusiness, IWebHostEnvironment env,IUsersBusiness usersBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            //_profileBusiness = profileBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _env = env;
            _usersBusiness = usersBusiness;
            _jwtBusiness = jwtBusiness;


        }

        public IActionResult Index()
        {
            //Testupload();
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                //loadCountries = true,
                //loadProfileTypes = true,
                //loadDocumentTypes = true
            });
            return View(response);
        }

        public IActionResult Create(int userid)
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadFormats = true,
                loadUserCategory = true,
                loadRoundingRule = true,
                loadDestinations = true,
                loadSuperAgents = true,
                loadCurrencyRate=true
            });
            ViewData["loadDataCreate"] = response;
            ViewData["userid"] = userid.ToString();


            return View();
            
        }

        public IActionResult Contract()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                //loadProducts = true,
                //loadProfiles = true
            });
            return View(response);
        }

        //[HttpPost]
        //public SearchProfilesResp Search(SearchProfilesReq req)
        //{
        //    return null;
        //    //return _profileBusiness.SearchProfiles(req);
        //}

       public IActionResult Reset()
        {
            return View();
        }
        [HttpPost]
        public ResetPass resetPassword(ResetPass pass)
        {
                pass.userId = _user.U_Id;
                var resp= _usersBusiness.resetPass(pass);
                return resp;
        }
        [HttpPost]
        public UsersResp createNewUser(UsersReq req)
        {
            var response = new UsersResp();
            if (req.Parent_Id == null)
                req.Parent_Id = _user.U_Id;
            if (req != null)
            {
                 response = _usersBusiness.ModifyUser(req, "Create", _user.U_Id);

            }
            else
            {
                response.statusCode.code = 0;
                response.statusCode.message = "Fail to insert User!";
            }

            return response;

        }
        [HttpGet]
        public UsersSearchResp GetUsersList(string name,int? parentid)
        {
            //user.Id = _user.UserId;
            var user = new UsersSearchReq();
            user.First_Name = name;
            if (parentid == null)
                user.Parent_Id = _user.U_Id;
            else 
                user.Parent_Id = parentid;
            var response = new UsersSearchResp();
            response.users = _usersBusiness.GetUsersList(user);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, user.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }
        //public IActionResult createUser(int userid)
        //{
        //    LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
        //    {
        //        loadFormats=true,
        //        loadUserCategory=true,
        //        loadRoundingRule=true,
        //        loadDestinations=true,
        //        loadSuperAgents=true
        //    });
        //    ViewData["loadDataCreate"] = response;
        //    ViewData["userid"] = userid.ToString();

            
        //    return View();
        //}
        public IActionResult Details(int userid)
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadFormats=true
                //loadCountries = true,
                //loadProfileTypes = true,
                //loadDocumentTypes = true
            });
            var user = _usersBusiness.GetUser(userid);
            ViewData["loadDataCreate"] = response;
            //ViewData["userid"] = userid.ToString();

            
            return View(user);
        }
        [HttpPost]
        public UsersResp EditUser(UsersReq req)
        {
            var response = new UsersResp();
            if (req.Id == 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

          


            return _usersBusiness.ModifyUser(req, "Update", _user.U_Id);
        }

        [HttpPost]
        public UsersResp DeleteUsers(int id)
        {
            var req = new UsersReq();
            req.Id = id;

            return _usersBusiness.ModifyUser(req, "Delete", _user.U_Id);
        }
        public UsersResp GetUserById(int userId)
        {
            var response = _usersBusiness.GetUser(userId);

            return response;
        }
        [HttpGet]
        public CookieUser ValidateUser()
        {
            try
            {
                string token = _httpContextAccessor.HttpContext.Request.Headers["token"].ToString();
                return _jwtBusiness.getUserFromToken(token, _appSettings.jwt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public string getUserPass(int userid)
        {
           
                return _usersBusiness.getUserPass(userid);
          
        }
    }
}

