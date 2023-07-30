using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Tariff;
using ProjectX.Business.Profile;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Tariff;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace ProjectX.Controllers
{
    public class TariffController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ITariffBusiness _tariffBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;



        public TariffController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, ITariffBusiness tariffBusiness, IGeneralBusiness generalBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _tariffBusiness = tariffBusiness;
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
                loadPlans = true,
            });

            //  throw new Exception("This is an example error.");

            return View(response);
        }

        [HttpPost]
        public TariffSearchResp Search(TariffSearchReq req)
        {
            TariffSearchResp response = new TariffSearchResp();
            response.tariff = _tariffBusiness.GetTariffList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {
            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadPlans = true,
            });
            ViewData["filldata"] = load;

            TariffGetResp response = new TariffGetResp();
            response.tariff = new TR_Tariff();
            return View(response);
        }


        [HttpPost]
        public TariffResp CreateTariff(TariffReq req)
        {
            TariffResp response = new TariffResp();
            //if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
            //    return response;
            //}

            return _tariffBusiness.ModifyTariff(req, "Create", _user.U_Id);
        }


        public ActionResult Edit(int id)
        {
            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadPlans = true,
            });
            ViewData["filldata"] = load;

            TariffResp response = new TariffResp();
            response = _tariffBusiness.GetTariff(id);

            return View("details", response);
        }

        [HttpPost]
        public TariffResp EditTariff(TariffReq req)
        {
            TariffResp response = new TariffResp();
            if (req.id == 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            //if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
            //    return response;
            //}


            return _tariffBusiness.ModifyTariff(req, "Update", _user.U_Id);
        }

        [HttpPost]
        public TariffResp DeleteTariff(int id)
        {
            TariffReq req = new TariffReq();
            req.id = id;
            req.tariff_starting_date = DateTime.Today;
            TariffResp response = new TariffResp();
            return _tariffBusiness.ModifyTariff(req, "Delete", _user.U_Id);
        }
    }
}
