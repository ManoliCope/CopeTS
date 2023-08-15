﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Production;
using ProjectX.Business.Profile;
using ProjectX.Controllers;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Package;
using ProjectX.Entities.Models.Product;
using ProjectX.Entities.Models.Production;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using static ProjectX.Controllers.ProductionController;
using System.IO.Packaging;

namespace ProjectX.Controllers
{
    public class ProductionController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IProductionBusiness _productionBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;

        private IWebHostEnvironment _env;

        public ProductionController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IProductionBusiness productionBusiness, IGeneralBusiness generalBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _productionBusiness = productionBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _env = env;
        }


        // GET: ProductionController
        [HttpGet]
        public ActionResult Index()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadBenefits = true,
                loadProducts = true,
                loadDestinations = true,
                loadPlans = true,
                loadTariffs = true

            });
            return View(response);
        }

        // GET: ProductionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        public ActionResult Create()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadBenefits = true,
                loadProducts = true,
                loadDestinations = true,
                loadPlans = true,
                loadTariffs = true,
                loadZones = true

            });

            ViewData["filldata"] = response;

            ProdGetResp ttt = new ProdGetResp();
            ttt.product = new TR_Product();
            return View(ttt);
        }


        [HttpPost]
        public ProductionSearchResp Search(ProductionSearchReq req)
        {
            ProductionSearchResp response = new ProductionSearchResp();
            response.Production = _productionBusiness.GetPoliciesList( req, _user.U_Id);

            return response;
        }


        // GET: ProductionController/Create
        public ActionResult Createbak()
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = new LoadDataModel();
            ViewData["filldata"] = response;

            ProdGetResp ttt = new ProdGetResp();
            ttt.product = new TR_Product();
            return View(ttt);
        }

        // POST: ProductionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductionController/Edit/5
        public ActionResult Edit(int id)
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadBenefits = true,
                loadProducts = true,
                loadDestinations = true,
                loadPlans = true,
                loadTariffs = true,
                loadZones = true

            });
            ViewData["filldata"] = response;

            ProductionPolicy policyreponse = new ProductionPolicy();
            policyreponse = _productionBusiness.GetPolicy(id, _user.U_Id);

            if (policyreponse != null)
            {
                int typeid = 0;
                if (policyreponse.IsIndividual)
                    typeid = 1;
                else if (policyreponse.IsFamily)
                    typeid = 2;
                else if (policyreponse.IsGroup)
                    typeid = 3;

                List<TR_Product> productlist = GetProdutctsByType(typeid);
                List<TR_Zone> zonelist = GetZonesByProduct(policyreponse.ProductID);
                List<TR_Destinations> destinationlist = GetDestinationByZone(policyreponse.ZoneID);
                List<TR_Benefit> benefitlist = GetAdditionalBenbyTariff(policyreponse.PolicyDetails.Select(detail => detail.Tariff).ToList());

                ViewData["productlist"] = productlist;
                ViewData["zonelist"] = zonelist;
                ViewData["destinationlist"] = destinationlist;
                ViewData["benefitlist"] = benefitlist;

                return View("details", policyreponse);
            }
            else
                return RedirectToAction("index", "Production"); // Redirect to another action

        }

        // POST: ProductionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: ProductionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public List<TR_Product> GetProdutctsByType(int id) //individual,family,group
        {
            List<TR_Product> response = new List<TR_Product>();
            return _productionBusiness.GetProductsByType(id);
        }
        public List<TR_Zone> GetZonesByProduct(int id) //individual,family,group
        {
            List<TR_Zone> response = new List<TR_Zone>();
            return _productionBusiness.GetZonesByProduct(id);
        }

        public List<TR_Destinations> GetDestinationByZone(int ZoneId)
        {
            List<TR_Destinations> response = new List<TR_Destinations>();
            return _productionBusiness.GetDestinationByZone(ZoneId);
        }

        public List<TR_Benefit> GetAdditionalBenbyTariff(List<int> Tariff)
        {
            List<TR_Benefit> response = new List<TR_Benefit>();
            return _productionBusiness.GetAdditionalBenbyTariff(Tariff);
        }

        [HttpPost]
        public ProductionResp GetQuotation(List<ProductionReq> quotereq)
        {
            return _productionBusiness.getProductionDetails(quotereq, _user.U_Id);
        }

        [HttpPost]
        public IActionResult GetPartialViewQuotation(ProductionResp quotereq)
        {
            return PartialView("~/Views/partialviews/partialquotationlist.cshtml", quotereq);

        }

        [HttpPost]
        public ProductionSaveResp IssuePolicy(IssuanceReq IssuanceReq)
        {
            return _productionBusiness.SaveIssuance(IssuanceReq, _user.U_Id);
        }
    }
}
