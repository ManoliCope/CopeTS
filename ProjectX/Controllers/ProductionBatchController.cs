using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
        public ProductionBatchSaveResp importproduction(string importedbatch,string title)
        {
            ProductionBatchSaveReq reqq = new ProductionBatchSaveReq();
            List<ProductionBatchDetailsReq> productionBatchDetailsList = DeserializeJsonString(importedbatch);
            reqq.productionbatches = productionBatchDetailsList;
            reqq.title = title;
            reqq.userid = _user.U_Id;
            //return null;
            return _productionBatchBusiness.SaveProductionBatch(reqq);
        }
        public List<ProductionBatchDetailsReq> DeserializeJsonString(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<ProductionBatchDetailsReq>>(jsonString);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public List<ProductionBatchDetailsReq> exceltotable([FromForm(Name = "files")] IFormFileCollection files)
        {
            List<ProductionBatchDetailsReq> policies = new List<ProductionBatchDetailsReq>();

            foreach (IFormFile formFile in files)
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = new System.IO.MemoryStream())
                {

                    formFile.CopyTo(stream);
                    stream.Position = 0;
                    using (var reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream))
                    {
                        while (reader.Read())
                        {
                            if (reader.Depth != 0)
                            {
                                policies.Add(new ProductionBatchDetailsReq
                                {
                                    //FirstName = reader.GetValue(6) == null ? "" : reader.GetValue(6).ToString(),

                                    ReferenceNumber = reader.GetValue(0).ToString()??"",
                                    Type = reader.GetValue(1).ToString() ?? "",
                                    Plan = reader.GetValue(2).ToString() ?? "",
                                    Zone = reader.GetValue(3).ToString() ?? "",
                                    Days = Convert.ToInt16(reader.GetValue(4)),
                                    StartDate = Convert.ToDateTime(reader.GetValue(5)),
                                    FirstName = reader.GetValue(6).ToString() ?? "",
                                    MiddleName = reader.GetValue(7).ToString() ?? "",
                                    LastName = reader.GetValue(8).ToString() ?? "",
                                    DateOfBirth = Convert.ToDateTime(reader.GetValue(9)),
                                    Age = Convert.ToInt16(reader.GetValue(10)),
                                    Gender = reader.GetValue(11).ToString() ?? "",
                                    PassportNumber = reader.GetValue(12).ToString() ?? "",
                                    Nationality = reader.GetValue(13).ToString() ?? "",
                                    PremiumInUSD = Convert.ToDecimal(reader.GetValue(14)),
                                    NetInUSD = Convert.ToDecimal(reader.GetValue(15))
                                });
                            }
                        }
                    }
                }
            }
            return policies;
        }
    }
}
