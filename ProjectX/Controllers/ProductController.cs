using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Product;
using ProjectX.Business.Profile;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Product;
using ProjectX.Entities.Models.Profile;
using ProjectX.Entities.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;

namespace ProjectX.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IProductBusiness _productBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private User _user;



        public ProductController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IProductBusiness productBusiness, IGeneralBusiness generalBusiness)
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
        public ProdSearchResp Search(ProdSearchReq req)
        {
            ProdSearchResp response = new ProdSearchResp();
            response.products = _productBusiness.GetProductList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = new LoadDataModel();
            ViewData["filldata"] = response;

            ProdGetResp ttt = new ProdGetResp();
            ttt.product = new TR_Product();
            return View(ttt);
        }


        [HttpPost]
        public ProdResp CreateProduct(ProdReq req)
        {
            ProdResp response = new ProdResp();
            if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            return _productBusiness.ModifyProduct(req, "Create", _user.UserId);
        }


        public ActionResult Edit(int id)
        {
            ProdResp response = new ProdResp();
            response = _productBusiness.GetProduct(id);

            return View("details", response);
        }

        [HttpPost]
        public ProdResp EditProduct(ProdReq req)
        {
            ProdResp response = new ProdResp();
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


            return _productBusiness.ModifyProduct(req, "Update", _user.UserId);
        }

        [HttpPost]
        public ProdResp DeleteProduct(int id)
        {
            ProdReq req = new ProdReq();
            req.id = id;
            DateTime thisDay = DateTime.Today;

            req.activation_date = thisDay;
            ProdResp response = new ProdResp();
            return _productBusiness.ModifyProduct(req, "Delete", _user.UserId);
        }
    }
}
