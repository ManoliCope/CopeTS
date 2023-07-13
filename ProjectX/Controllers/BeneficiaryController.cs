using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Beneficiary;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Beneficiary;
using ProjectX.Entities.Resources;


namespace ProjectX.Controllers
{
    public class BeneficiaryController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IBeneficiaryBusiness _beneficiaryBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private User _user;

        private IWebHostEnvironment _env;


        public BeneficiaryController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IBeneficiaryBusiness beneficiaryBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _beneficiaryBusiness = beneficiaryBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (User)httpContextAccessor.HttpContext.Items["User"];
            _env = env;

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
        public BeneficiarySearchResp Search(BeneficiarySearchReq req)
        {
            BeneficiarySearchResp response = new BeneficiarySearchResp();
            response.beneficiary = _beneficiaryBusiness.GetBeneficiaryList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = new LoadDataModel();
            ViewData["filldata"] = response;

            BeneficiaryGetResp ttt = new BeneficiaryGetResp();
            ttt.beneficiary = new TR_Beneficiary();
            return View(ttt);
        }


        [HttpPost]
        public BeneficiaryResp CreateBeneficiary(BeneficiaryReq req)
        {
            BeneficiaryResp response = new BeneficiaryResp();
            if (string.IsNullOrEmpty(req.FirstName) || string.IsNullOrWhiteSpace(req.FirstName))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            return _beneficiaryBusiness.ModifyBeneficiary(req, "Create", _user.UserId);
        }


        public ActionResult Edit(int id)
        {
            BeneficiaryResp response = new BeneficiaryResp();
            response = _beneficiaryBusiness.GetBeneficiary(id);

            return View("details", response);
        }

        [HttpPost]
        public BeneficiaryResp EditBeneficiary(BeneficiaryReq req)
        {
            BeneficiaryResp response = new BeneficiaryResp();
            if (req.Id == 0)
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            if (string.IsNullOrEmpty(req.FirstName) || string.IsNullOrWhiteSpace(req.FirstName))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }


            return _beneficiaryBusiness.ModifyBeneficiary(req, "Update", _user.UserId);
        }

        [HttpPost]
        public BeneficiaryResp DeleteBeneficiary(int id)
        {
            BeneficiaryReq req = new BeneficiaryReq();
            req.Id = id;

            return _beneficiaryBusiness.ModifyBeneficiary(req, "Delete", _user.UserId);
        }
    }
}
