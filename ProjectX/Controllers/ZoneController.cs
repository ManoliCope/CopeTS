using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Zone;
using ProjectX.Business.Profile;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Zone;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace ProjectX.Controllers
{
    public class ZoneController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IZoneBusiness _productBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;



        public ZoneController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IZoneBusiness productBusiness, IGeneralBusiness generalBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _productBusiness = productBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
        }


        // GET: CobController
        public ActionResult Index()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                //loadCountries = true,
                //loadProfileTypes = true,
                //loadDocumentTypes = true
            });


            return View(response);
        }

        [HttpPost]
        public ZoneSearchResp Search(ZoneSearchReq req)
        {
            ZoneSearchResp response = new ZoneSearchResp();
            response.zone = _productBusiness.GetZoneList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadDestinations = true,
            });
            ViewData["filldata"] = response;

            ZoneGetResp ttt = new ZoneGetResp();
            //ttt.product = new TR_Zone();
            return View(ttt);
        }


        [HttpPost]
        public ZoneResp CreateZone(ZoneReq req)
        {
            ZoneResp response = new ZoneResp();
            if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            return _productBusiness.ModifyZone(req, "Create", _user.U_Id);
        }


        public ActionResult Edit(int id)
        {
            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadDestinations = true,
            });
            ViewData["filldata"] = load;


            ZoneResp response = new ZoneResp();
            response = _productBusiness.GetZone(id);
            return View("details", response);
        }

        [HttpPost]
        public ZoneResp EditZone(ZoneReq req)
        {
            ZoneResp response = new ZoneResp();
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


            return _productBusiness.ModifyZone(req, "Update", _user.U_Id);
        }

        [HttpPost]
        public ZoneResp DeleteZone(int id)
        {
            ZoneReq req = new ZoneReq();
            req.id = id;
            DateTime thisDay = DateTime.Today;

            //req.activation_date = thisDay;
            ZoneResp response = new ZoneResp();
            return _productBusiness.ModifyZone(req, "Delete", _user.U_Id);
        }
    }
}
