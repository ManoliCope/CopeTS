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
        private IBenefitBusiness _benefitBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;



        public BenefitController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IBenefitBusiness benefitBusiness, IGeneralBusiness generalBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _benefitBusiness = benefitBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
        }


        // GET: CobController
        public ActionResult Index()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
            });


            return View(response);
        }

        [HttpPost]
        public BenSearchResp Search(BenSearchReq req)
        {
            BenSearchResp response = new BenSearchResp();
            response.benefit = _benefitBusiness.GetBenefitList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {

            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadFormats = true,
                loadBenefitTitle = true
            });

            ViewData["filldata"] = load;

            BenGetResp ttt = new BenGetResp();
            ttt.benefit = new TR_Benefit();
            return View(ttt);
        }


        [HttpPost]
        public BenResp CreateBenefit(BenReq req)
        {
            BenResp response = new BenResp();
            if (req.titleId > 0)
                return _benefitBusiness.ModifyBenefit(req, "Create", _user.U_Id);


            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
            return response;
        }


        public ActionResult Edit(int id)
        {
            BenResp response = new BenResp();
            response = _benefitBusiness.GetBenefit(id);

            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadFormats = true,
                loadBenefitTitle = true


            });

            ViewData["filldata"] = load;



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

            if (req.titleId == 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }


            return _benefitBusiness.ModifyBenefit(req, "Update", _user.U_Id);
        }

        [HttpPost]
        public BenResp DeleteBenefit(int id)
        {
            BenReq req = new BenReq();
            req.id = id;
            DateTime thisDay = DateTime.Today;

            //req.date = thisDay;
            BenResp response = new BenResp();
            return _benefitBusiness.ModifyBenefit(req, "Delete", _user.U_Id);
        }
    }
}
