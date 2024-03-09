using Microsoft.AspNetCore.Http;
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
using ProjectX.Business.Users;
using ProjectX.Repository.UsersRepository;
using SelectPdf;
using ProjectX.Entities.Models.Users;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace ProjectX.Controllers
{
    public class ProductionController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IProductionBusiness _productionBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private IUsersBusiness _usersBusiness;
        private TR_Users _user;
        private IUsersRepository _usersRepository;

        private IWebHostEnvironment _env;

        public ProductionController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IUsersBusiness usersBusiness, IProductionBusiness productionBusiness, IGeneralBusiness generalBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _productionBusiness = productionBusiness;
            _generalBusiness = generalBusiness;
            _usersBusiness = usersBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _env = env;
        }


        // GET: ProductionController
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);


            var response = _usersBusiness.GetUsersChildren(_user.U_Id);
            ViewData["userid"] = _user.U_Id.ToString();
            return View(response);
        }

        public ActionResult Details(int id)
        {
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);


            return View();
        }

        public ActionResult Create()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadBenefits = true,
                loadDestinations = true,
                loadPlans = true,
                loadTariffs = true,
                loadZones = true

            });




            ViewData["filldata"] = response;
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);

            ProdGetResp ttt = new ProdGetResp();
            ttt.product = new TR_Product();
            return View(ttt);
        }
        public ActionResult ManualProduction()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadBenefits = true,
                loadDestinations = true,
                loadPlans = true,
                loadTariffs = true,
                loadZones = true

            });




            ViewData["filldata"] = response;
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);

            ProdGetResp ttt = new ProdGetResp();
            ttt.product = new TR_Product();
            return View(ttt);
        }


        [HttpPost]
        public ProductionSearchResp Search(ProductionSearchReq req)
        {
            ProductionSearchResp response = new ProductionSearchResp();
            response.Production = _productionBusiness.GetPoliciesList(req, _user.U_Id);

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

        public ActionResult EditManual(Guid id)
        {
            int policyid = _productionBusiness.GetPolicyID(id, _user.U_Id);

            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);

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
            policyreponse = _productionBusiness.GetPolicy(policyid, _user.U_Id, false);

            if (policyreponse != null)
            {
                int typeid = 0;
                if (policyreponse.IsIndividual)
                    typeid = 1;
                else if (policyreponse.IsFamily)
                    typeid = 2;
                else if (policyreponse.IsGroup)
                    typeid = 3;

                List<TR_Product> productlist = GetProdutctsByType(typeid, policyid);
                List<TR_Zone> zonelist = GetZonesByProduct(policyreponse.ProductID);
                List<TR_Destinations> destinationlist = GetDestinationByZone(policyreponse.ZoneID);
                List<TR_Benefit> benefitlist = GetAdditionalBenbyTariff(policyreponse.PolicyDetails.Select(detail => detail.Tariff).ToList());

                ViewData["productlist"] = productlist;
                ViewData["zonelist"] = zonelist;
                ViewData["destinationlist"] = destinationlist;
                ViewData["benefitlist"] = benefitlist;
                ViewData["isAdmin"] = _user.U_Is_Admin.ToString();

                return View("detailsManual", policyreponse);
            }
            else
                return RedirectToAction("index", "Production"); // Redirect to another action

        }



        // GET: ProductionController/Edit/5
        public ActionResult Edit(Guid id)
        {
            int policyid = _productionBusiness.GetPolicyID(id, _user.U_Id);
            if (policyid == 0)
                return RedirectToAction("Index");

            UserRights UserRights = _usersBusiness.GetUserRights(_user.U_Id);
            ViewData["userrights"] = UserRights;


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
            policyreponse = _productionBusiness.GetPolicy(policyid, _user.U_Id, false);

            if (policyreponse != null)
            {
                int typeid = 0;
                if (policyreponse.IsIndividual)
                    typeid = 1;
                else if (policyreponse.IsFamily)
                    typeid = 2;
                else if (policyreponse.IsGroup)
                    typeid = 3;

                List<TR_Product> productlist = GetProdutctsByType(typeid, policyreponse.PolicyID);
                List<TR_Zone> zonelist = GetZonesByProduct(policyreponse.ProductID);
                List<TR_Destinations> destinationlist = GetDestinationByZone(policyreponse.ZoneID);
                List<TR_Benefit> benefitlist = GetAdditionalBenbyTariff(policyreponse.PolicyDetails.Select(detail => detail.Tariff).ToList());

                ViewData["productlist"] = productlist;
                ViewData["zonelist"] = zonelist;
                ViewData["destinationlist"] = destinationlist;
                ViewData["benefitlist"] = benefitlist;
                ViewData["isAdmin"] = _user.U_Is_Admin.ToString();


                UserRights CreatedByUserRights = _usersBusiness.GetUserRights(policyreponse.CreatedById);
                ViewData["CreatedByuserrights"] = CreatedByUserRights;


                if (!policyreponse.IsCanceled && (policyreponse.IsEditable || UserRights.can_edit == true))
                    ViewData["canedit"] = "1";
                else
                    ViewData["canedit"] = "0";

                return View("details", policyreponse);
            }
            else
                return RedirectToAction("index", "Production"); // Redirect to another action

        }

        // POST: ProductionController/Edit/5
        [HttpPost]
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


        public List<TR_Product> GetProdutctsByType(int id, int policyid) //individual,family,group
        {
            List<TR_Product> response = new List<TR_Product>();
            return _productionBusiness.GetProductsByType(id, policyid, _user.U_Id);
        }
        public List<TR_Zone> GetZonesByProduct(int id)
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
        public IActionResult GetPartialViewQuotation(ProductionResp quotereq, int isnew, int polId)
        {
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);

            int createdby = _user.U_Id;
            if (polId != 0)
            {
                var getpolicy = _productionBusiness.GetPolicy(polId, _user.U_Id, false);
                createdby = getpolicy.CreatedById;
            }

            UserRights CreatedByUserRights = _usersBusiness.GetUserRights(createdby);
            ViewData["CreatedByuserrights"] = CreatedByUserRights;

            ViewData["isnew"] = isnew;
            return PartialView("~/Views/partialviews/partialquotationlist.cshtml", quotereq);
        }
        [HttpPost]
        public IActionResult GetPartialViewQuotationFamily(ProductionResp quotereq, int isnew, int polId)
        {
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);

            int createdby = _user.U_Id;
            if (polId != 0)
            {
                var getpolicy = _productionBusiness.GetPolicy(polId, _user.U_Id, false);
                createdby = getpolicy.CreatedById;
            }

            UserRights CreatedByUserRights = _usersBusiness.GetUserRights(createdby);
            ViewData["CreatedByuserrights"] = CreatedByUserRights;

            ViewData["isnew"] = isnew;
            return PartialView("~/Views/partialviews/partialquotationlist-family.cshtml", quotereq);
        }
        public List<TR_Beneficiary> GetPolicyBeneficiaries(int id)
        {
            List<TR_Beneficiary> policyreponse = new List<TR_Beneficiary>();
            policyreponse = _productionBusiness.GetPolicyBeneficiaries(id, _user.U_Id);

            return policyreponse;
        }

        [HttpPost]
        public ProductionSaveResp IssuePolicy(IssuanceReq IssuanceReq)
        {
            ProductionSaveResp response = new ProductionSaveResp();

            DateTime currentDate = DateTime.Now.Date;
            DateTime fromdate = Convert.ToDateTime(IssuanceReq.from).Date;
            if (fromdate < currentDate && (_user.U_Is_Admin == false))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.Backdate);
                return response;
            }

            return _productionBusiness.SaveIssuance(IssuanceReq, _user.U_Id);
        }


        [HttpPost]
        public IActionResult ConvertHtmlToPdf([FromBody] string htmlContent)
        {
            // Create a new HTML to PDF converter object
            HtmlToPdf converter = new HtmlToPdf();

            // Convert HTML to PDF
            PdfDocument doc = converter.ConvertHtmlString(htmlContent);

            // Save the PDF as a byte array
            byte[] pdfBytes;
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream);
                pdfBytes = pdfStream.ToArray();
            }

            // Close the PDF document
            doc.Close();

            // Return the PDF as a file
            return File(pdfBytes, "application/pdf", "converted.pdf");
        }

        [HttpPost]
        public ProductionResp CancelProduction(int polId)
        {
            return _productionBusiness.CancelProduction(polId, _user.U_Id);
        }
        [HttpPost]
        public ProductionResp EditableProduction(int polId, int isEditable)
        {
            var editable = isEditable == 1 ? true : false;

            return _productionBusiness.EditableProduction(polId, _user.U_Id, editable);
        }
    }
}
