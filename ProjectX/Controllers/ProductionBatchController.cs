using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProjectX.Business.General;
using ProjectX.Entities;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.ProductionBatch;
using ProjectX.Entities.Resources;
using ProjectX.Repository.ProductionBatch;

namespace ProjectX.Controllers
{
    public class ProductionBatchController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IProductionBatchBusiness _productionBatchBusiness;
        private IGeneralBusiness _generalBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;

        private IWebHostEnvironment _env;


        public ProductionBatchController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IProductionBatchBusiness productionBatchBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _productionBatchBusiness = productionBatchBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ProductionBatchSearchResp Search(ProductionBatchSearchReq req)
        {
            ProductionBatchSearchResp response = new ProductionBatchSearchResp();
            response.productionbatch = _productionBatchBusiness.GetProductionBatchList(req);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Case");

            return response;
        }

    }
}
