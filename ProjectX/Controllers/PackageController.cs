﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Package;
using ProjectX.Business.Profile;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Package;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace ProjectX.Controllers
{
    public class PackageController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IPackageBusiness _packageBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;



        public PackageController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IPackageBusiness productBusiness, IGeneralBusiness generalBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _packageBusiness = productBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
        }


        // GET: CobController
        public ActionResult Index()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadZones= true,
                loadProducts = true
            });


            return View(response);
        }

        [HttpPost]
        public PackSearchResp Search(PackSearchReq req)
        {
            PackSearchResp response = new PackSearchResp();
            response.package = _packageBusiness.GetPackageList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {
            PackGetResp response = new PackGetResp();
            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadZones = true,
                loadProducts = true
            });

            response.package = new TR_Package();
            response.loadedData = load.loadedData;
            return View(response);
        }


        [HttpPost]
        public PackResp CreatePackage(PackReq req)
        {
            PackResp response = new PackResp();
            
            //if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
            //    return response;
            //}

            return _packageBusiness.ModifyPackage(req, "Create", _user.U_Id);
        }


        public ActionResult Edit(int id)
        {
            LoadDataResp load = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadZones = true,
                loadProducts = true
            });

            ViewData["filldata"] = load;

            PackResp response = new PackResp();
            response = _packageBusiness.GetPackage(id);

            return View("details", response);
        }

        [HttpPost]
        public PackResp EditPackage(PackReq req)
        {
            PackResp response = new PackResp();
            if (req.Id == 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            //if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
            //    return response;
            //}


            return _packageBusiness.ModifyPackage(req, "Update", _user.U_Id);
        }

        [HttpPost]
        public PackResp DeletePackage(int id)
        {
            PackReq req = new PackReq();
            req.Id = id;
            DateTime thisDay = DateTime.Today;

            //req.activation_date = thisDay;
            PackResp response = new PackResp();
            return _packageBusiness.ModifyPackage(req, "Delete", _user.U_Id);
        }
    }
}
