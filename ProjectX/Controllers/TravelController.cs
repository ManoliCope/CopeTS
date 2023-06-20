using Microsoft.AspNetCore.Mvc;
//using ProjectX.Entities.Models.Travel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProjectX.Entities.AppSettings;
using ProjectX.Business.General;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.dbModels;

namespace ProjectX.Controllers
{
    public class TravelController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private User _user;

        public TravelController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness)
        {
            _httpContextAccessor = httpContextAccessor;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (User)httpContextAccessor.HttpContext.Items["User"];
        }
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Search(TravelSearchReq req)
        //{
        //    return PartialView();
        //}

        public IActionResult Search()
        {
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadCustomerTypes = true
            });
            return View(response);
        }




        public IActionResult testeditor()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult GetCall(GetCallReq req)
        //{
        //    GetCallResp result = _travelBusiness.GetCallInfo(req);
        //    //return RedirectToActionPermanentPreserveMethod("Index", "RegisterCall", new { result });
        //    //RegisterCallController registerCallController;
        //    //return registerCallController.Index(result);
        //    //new RegisterCallController(_httpContextAccessor, null, _travelBusiness, _generalBusiness).Index(result);

        //    result.loadedData = _generalBusiness.loadData(new ProjectX.Entities.bModels.LoadDataModelSetup
        //    {
        //        loadCaseStatuses = true,
        //        loadCustomerTypes = true,
        //        loadCaseTypes = true,
        //        loadCaseLevels = true
        //    }).loadedData;

        //    return PartialView("~/views/RegisterCall/Index.cshtml", result);
        //}

        //[HttpPost]
        //public SaveCallResp SaveCall(SaveCallReq req)
        //{
        //    return _travelBusiness.SaveCall(req, _user.Username);
        //}

        //[HttpPost]
        //public IActionResult testt([FromBody] string from, string take)
        //{
        //    return BadRequest(new { Result = false, Message = "" });
        //}

        [HttpPost]
        public string ViewPolicy(int policyId)
        {
            return "";
        }
    }
}