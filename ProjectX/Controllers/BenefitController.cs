using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Benefit;
using ProjectX.Business.Profile;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Benefit;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace ProjectX.Controllers
{
    public class BenefitController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IBenefitBusiness _productBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private User _user;



        public BenefitController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IBenefitBusiness productBusiness, IGeneralBusiness generalBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _productBusiness = productBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (User)httpContextAccessor.HttpContext.Items["User"];
        }


        // GET: CobController
        public ActionResult Index()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadCountries = true,
                loadProfileTypes = true,
                loadDocumentTypes = true
            });


            return View(response);
        }

        [HttpPost]
        public BenSearchResp Search(BenSearchReq req)
        {
            BenSearchResp response = new BenSearchResp();
            response.benefit = _productBusiness.GetBenefitList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = new LoadDataModel();
            ViewData["filldata"] = response;

            BenGetResp ttt = new BenGetResp();
            ttt.benefit = new TR_Benefit();
            return View(ttt);
        }


        [HttpPost]
        public BenResp CreateBenefit(BenReq req)
        {
            BenResp response = new BenResp();
            if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            return _productBusiness.ModifyBenefit(req, "Create", _user.UserId);
        }


        public ActionResult Edit(int id)
        {
            BenResp response = new BenResp();
            response = _productBusiness.GetBenefit(id);

            return View("details", response);
        }

        [HttpPost]
        public BenResp EditBenefit(BenReq req)
        {
            BenResp response = new BenResp();
            if (req.id == 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }


            return _productBusiness.ModifyBenefit(req, "Update", _user.UserId);
        }

        [HttpPost]
        public BenResp DeleteBenefit(int id)
        {
            BenReq req = new BenReq();
            req.id = id;
            DateTime thisDay = DateTime.Today;

            //req.date = thisDay;
            BenResp response = new BenResp();
            return _productBusiness.ModifyBenefit(req, "Delete", _user.UserId);
        }
    }
}
