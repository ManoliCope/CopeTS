
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.Plan;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Plan;
using ProjectX.Entities.Resources;


namespace ProjectX.Controllers
{
    public class PlanController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IPlanBusiness _planBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;

        private IWebHostEnvironment _env;


        public PlanController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IPlanBusiness planBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _planBusiness = planBusiness;
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
        public PlanSearchResp Search(PlanSearchReq req)
        {
            PlanSearchResp response = new PlanSearchResp();
            response.plan = _planBusiness.GetPlanList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = new LoadDataModel();
            ViewData["filldata"] = response;

            PlanGetResp ttt = new PlanGetResp();
            ttt.plan = new TR_Plan();
            return View(ttt);
        }


        [HttpPost]
        public PlanResp CreatePlan(PlanReq req)
        {
            PlanResp response = new PlanResp();
            if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            {
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
                return response;
            }

            return _planBusiness.ModifyPlan(req, "Create", _user.U_Id);
        }


        public ActionResult Edit(int id)
        {
            PlanResp response = new PlanResp();
            response = _planBusiness.GetPlan(id);

            return View("details", response);
        }

        [HttpPost]
        public PlanResp EditPlan(PlanReq req)
        {
            PlanResp response = new PlanResp();
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


            return _planBusiness.ModifyPlan(req, "Update", _user.U_Id);
        }

        [HttpPost]
        public PlanResp DeletePlan(int id)
        {
            PlanReq req = new PlanReq();
            req.id = id;

            return _planBusiness.ModifyPlan(req, "Delete", _user.U_Id);
        }
    }
}
