
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.CurrencyRate;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.CurrencyRate;
using ProjectX.Entities.Resources;


namespace ProjectX.Controllers
{
    public class CurrencyRateController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ICurrencyRateBusiness _currencyBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;

        private IWebHostEnvironment _env;


        public CurrencyRateController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, ICurrencyRateBusiness currBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _currencyBusiness = currBusiness;
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
              
            });


            return View(response);
        }

        [HttpPost]
        public CurrSearchResp Search(CurrSearchReq req)
        {
            CurrSearchResp response = new CurrSearchResp();
            response.currencyRate = _currencyBusiness.GetCurrencyRateList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }


        public ActionResult Create()
        {
            
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadCurrencies=true
            });

            ViewData["filldata"] = response;

            return View();
        }


        [HttpPost]
        public CurrResp CreateCurrencyRate(CurrReq req)
        {
            CurrResp response = new CurrResp();
            //if (string.IsNullOrEmpty(req.title) || string.IsNullOrWhiteSpace(req.title))
            //{
            //    response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidProfileName);
            //    return response;
            //}

            return _currencyBusiness.ModifyCurrencyRate(req, "Create", _user.U_Id);
        }


        public ActionResult Edit(int id)
        {
            LoadDataResp lists = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadCurrencies = true
            });

            ViewData["filldata"] = lists;

           var response = _currencyBusiness.GetCurrencyRate(id);

            return View("details", response);
        }

        [HttpPost]
        public CurrResp EditCurrencyRate(CurrReq req)
        {
            CurrResp response = new CurrResp();
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


            return _currencyBusiness.ModifyCurrencyRate(req, "Update", _user.U_Id);
        }

        [HttpPost]
        public CurrResp DeleteCurrencyRate(int id)
        {
            CurrReq req = new CurrReq();
            req.Id = id;

            return _currencyBusiness.ModifyCurrencyRate(req, "Delete", _user.U_Id);
        }
    }
}
