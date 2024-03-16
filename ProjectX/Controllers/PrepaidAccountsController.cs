using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Business.PrepaidAccounts;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.PrepaidAccounts;
using ProjectX.Entities.Resources;

namespace ProjectX.Controllers
{
    public class PrepaidAccountsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IPrepaidAccountsBusiness _prepaidAccountsBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;

        private IWebHostEnvironment _env;


        public PrepaidAccountsController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IPrepaidAccountsBusiness preAccBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _prepaidAccountsBusiness = preAccBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _env = env;

        }

        // GET: CobController
        public ActionResult Index()
        {
            var availableUsers = _prepaidAccountsBusiness.GetAvailableUsers(_user.U_Id);
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                
            });
            response.loadedData.users = availableUsers.users;

            return View(response);
        }

        [HttpPost]  
        public PreAccSearchResp GetUserBalance(int userid)
        {
            var response = new PreAccSearchResp();
            response = _prepaidAccountsBusiness.GetUserBalance(userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);

            return response;
        }
        [HttpPost]  
        public PreAccResp EditBalance(int action,float amount,int userid)
        {
            var response = new PreAccResp();
            response = _prepaidAccountsBusiness.EditBalance(_user.U_Id, action, amount, userid);
            //response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);

            return response;
        }
    }
}
