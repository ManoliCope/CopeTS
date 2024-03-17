using ProjectX.Entities;
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
using DocumentFormat.OpenXml.InkML;
using System.Security.Policy;

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
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _env;


        public UsersController(IHttpContextAccessor httpContextAccessor,
            IOptions<TrAppSettings> appIdentitySettingsAccessor,
            IGeneralBusiness generalBusiness, IJwtBusiness jwtBusiness, IWebHostEnvironment env, IUsersBusiness usersBusiness, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            //_profileBusiness = profileBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _env = env;
            _usersBusiness = usersBusiness;
            _jwtBusiness = jwtBusiness;
            _configuration = configuration;

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

        public IActionResult Create(Guid userid)
        {
            int iduser = _usersBusiness.GetUserID(userid);

            if (iduser == 0)
                iduser = _user.U_Id;
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadFormats = true,
                loadUserCategory = true,
                loadRoundingRule = true,
                loadDestinations = true,
                //loadSuperAgents = true,
                loadCurrencyRate = true
            });
            response.loadedData.superAgents = _usersBusiness.GetUsersChildren(iduser);
            ViewData["loadDataCreate"] = response;
            ViewData["userid"] = iduser.ToString();
            ViewData["isAdmin"] = _user.U_Is_Admin == true ? "1" : "0";

            if(iduser>0)
            {
                var parentuser = _usersBusiness.GetUser(iduser);
                var cancreate = parentuser.agents_Creation;
                //if(cancreate)

            }

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
            //pass.userId = _user.U_Id;
            var resp = _usersBusiness.resetPass(pass);
            return resp;
        }

        public List<TR_Users> credentialbyuser(int iduser)
        {
            //pass.userId = _user.U_Id;
            var resp = _usersBusiness.GetListedUserWithChildren(iduser);
            return resp;
        }

        [HttpPost]
        public UsersResp createNewUser(UsersReq req)
        {
            //var haveParent = true;
            var response = new UsersResp();
            if (req.Super_Agent_Id == null)
            {
                req.Super_Agent_Id = _user.U_Id;
                //haveParent = false;
            }
               
            if (req.Active == null)
                req.Active = true;
            if (req != null)
            {
                response = _usersBusiness.ModifyUser(req, "Create", _user.U_Id);
                if (response.Can_Inherit>0)
                {
                    var uploadsDirectory = _appSettings.UploadUsProduct.UploadsDirectory;
                    _usersBusiness.CopyParentAttachments(req.Super_Agent_Id??0, response.id, uploadsDirectory);
                }
                    
            }
            else
            {
                response.statusCode.code = 0;
                response.statusCode.message = "Fail to insert User!";
            }

            return response;

        }
        [HttpGet]
        public UsersSearchResp Search(string name, int? parentid)
        {
            //user.Id = _user.UserId;
            var user = new UsersSearchReq();
            user.User_Name = name;
            if (parentid == null)
                user.Super_Agent_Id = _user.U_Id;
            else
                user.Super_Agent_Id = parentid;
            var response = new UsersSearchResp();
            response = _usersBusiness.GetUsersList(user);
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
        public IActionResult Details(Guid userid, int su)
        {
            int iduser = _usersBusiness.GetUserID(userid);

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadFormats = true,
                loadUserCategory = true,
                loadRoundingRule = true,
                loadDestinations = true,
                //loadSuperAgents = true,
                loadCurrencyRate = true
            });
            response.loadedData.superAgents = _usersBusiness.GetUsersChildren(_user.U_Id);
            var user = _usersBusiness.GetUser(iduser);
            //ViewData["isAdmin"] = _user.U_Is_Admin == true ? "1" : "0";
            ViewData["loadDataCreate"] = response;
            ViewData["userid"] = _user.U_Id.ToString();
            ViewData["same"] = su.ToString();
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);

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

            return _usersBusiness.ModifyUser(req, "Delete", id);
        }
        public UsersResp GetUserById(Guid userId)
        {
            int iduser = _usersBusiness.GetUserID(userId);
            var response = _usersBusiness.GetUser(iduser);

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
        public IActionResult AssignProduct(Guid userid)
        {
            int iduser = _usersBusiness.GetUserID(userid);
            var user = _usersBusiness.GetUser(iduser);

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadProducts = true
            });

            ViewData["loadData"] = response;
            ViewData["userid"] = iduser.ToString();
            ViewData["name"] = user.first_Name + " " + user.last_Name;
            return View();
        }

        public UserProductResp deleteUsersProduct(int upid)
        {
            var req = new UsProReq();
            req.Action = "Delete";
            req.Id = upid;
            return _usersBusiness.ModifyUsersProduct(req);
        }
        public UsProSearchResp getUsersProduct(int userid)
        {

            var response = new UsProSearchResp();
            response.usersproduct = _usersBusiness.GetUsersProduct(userid);

            string requesturl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;
            response.Directory = Path.Combine(requesturl, _appSettings.ExternalFolder.Staticpathname, userid.ToString(), "conditions").Replace("\\", "/");

            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, userid == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");
            return response;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> assignUsersProduct([FromForm(Name = "files")] IFormFileCollection files,
            string Action, int ProductId, double IssuingFees, int UsersId)
        {
            //string uploadsDirectory = _configuration["UploadUsProduct:UploadsDirectory"];
            var uploadsDirectory = _appSettings.UploadUsProduct.UploadsDirectory;
            foreach (IFormFile file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var userFullPath = Path.Combine(uploadsDirectory, UsersId.ToString(), "Conditions");
                    createNewFolder(userFullPath);
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(userFullPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var req = new UsProReq
                    {
                        UsersId = UsersId,
                        ProductId = ProductId,
                        IssuingFees = IssuingFees,
                        Action = Action,
                        UploadedFile = fileName
                    };
                    var response = _usersBusiness.ModifyUsersProduct(req);
                    return Ok(response);
                }
                else
                {
                    return Json("No file selected.");
                }
            }
            return null;
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> saveUploadedLogo([FromForm(Name = "files")] IFormFileCollection files, int UsersId, string Folder)
        {//see tariff upload and make it like it.. make the popup as form
            //string uploadsDirectory = _configuration["UploadUsProduct:UploadsDirectory"];
            var uploadsDirectory = _appSettings.UploadUsProduct.UploadsDirectory;
            foreach (IFormFile file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var userFullPath = Path.Combine(uploadsDirectory, UsersId.ToString());
                    createNewFolder(userFullPath);
                    var logoPath = Path.Combine(userFullPath, Folder);
                    createNewFolder(logoPath);
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(logoPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save the file path to the database as needed
                    // Your database code goes here
                    var req = new UsProReq
                    {
                        UsersId = UsersId,
                        UploadedFile = fileName,
                        UploadedFolder = Folder
                    };

                    var response = _usersBusiness.SaveUploadedLogo(req);
                    response.UploadedFile = fileName;
                    return Ok(response);
                }
                else
                {
                    return Json("No file selected.");
                }
            }
            return null;
        }
        public void createNewFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }
        [HttpPost]
        public UserProductResp clearUploadedLogo(int userid)
        {
            return _usersBusiness.clearUploadedLogo(userid);
        }
    }
}

