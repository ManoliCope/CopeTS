
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.BenefitTitle;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.BenefitTitle;
using ProjectX.Entities.Resources;


namespace ProjectX.Controllers
{
    public class BenefitTitleController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IBenTitleBusiness _benTitleBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;

        private IWebHostEnvironment _env;


        public BenefitTitleController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IBenTitleBusiness benTitleBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _benTitleBusiness = benTitleBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
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
        public BenTitleSearchResp Search(BenTitleSearchReq req)
        {
            BenTitleSearchResp response = new BenTitleSearchResp();
            response.benefit_title = _benTitleBusiness.GetBenTitleList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = new LoadDataModel();
            ViewData["filldata"] = response;

            BenTitleGetResp ttt = new BenTitleGetResp();
            ttt.benefit_title = new TR_BenefitTitle();
            return View(ttt);
        }


        [HttpPost]
        public BenTitleResp CreateBenTitle(BenTitleReq req)
        {
            BenTitleResp response = new BenTitleResp();
            if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            return _benTitleBusiness.ModifyBenTitle(req, "Create", _user.U_Id);
        }


        public ActionResult Edit(int id)
        {
            BenTitleResp response = new BenTitleResp();
            response = _benTitleBusiness.GetBenTitle(id);

            return View("details", response);
        }

        [HttpPost]
        public BenTitleResp EditBenTitle(BenTitleReq req)
        {
            BenTitleResp response = new BenTitleResp();
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


            return _benTitleBusiness.ModifyBenTitle(req, "Update", _user.U_Id);
        }

        [HttpPost]
        public BenTitleResp DeleteBenTitle(int id)
        {
            BenTitleReq req = new BenTitleReq();
            req.id = id;

            return _benTitleBusiness.ModifyBenTitle(req, "Delete", _user.U_Id);
        }
    }
}
