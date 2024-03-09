using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProjectX.Business.General;
using ProjectX.Business.Jwt;
using ProjectX.Business.Report;
using ProjectX.Business.Users;
using ProjectX.Entities.AppSettings;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Report;
using System.Data;
using System.Text;
using ClosedXML.Excel;

namespace ProjectX.Controllers
{
    public class ReportController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUsersBusiness _usersBusiness;
        private IGeneralBusiness _generalBusiness;
        private IReportBusiness _reportBusiness;
        private readonly TrAppSettings _appSettings;
        private TR_Users _user;
        private IJwtBusiness _jwtBusiness;
        private IWebHostEnvironment _env;

        public ReportController(IHttpContextAccessor httpContextAccessor, IUsersBusiness usersBusiness, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IReportBusiness reportBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _reportBusiness = reportBusiness;
            _usersBusiness = usersBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _env = env;

        }
        public IActionResult Index()
        {
            ViewData["userrights"] = _usersBusiness.GetUserRights(_user.U_Id);
            return View();
        }

        static DataTable ConvertToDataTable(List<dynamic> dynamicList)
        {
            DataTable dataTable = new DataTable();

            if (dynamicList.Count > 0)
            {
                foreach (var property in ((IDictionary<string, object>)dynamicList[0]).Keys)
                {
                    dataTable.Columns.Add(property, typeof(object));
                }

                foreach (var item in dynamicList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    foreach (var property in ((IDictionary<string, object>)item).Keys)
                    {
                        dataRow[property] = ((IDictionary<string, object>)item)[property];
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }
            return dataTable;
        }
        public IActionResult ExporttoExcel(DataTable dataTable, string filename)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");
                worksheet.Cell(1, 1).InsertTable(dataTable);

                using (var memoryStream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);

                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename+".xlsx");
                }
            }
        }

        
        public ActionResult Production()
        {
            
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadProductionTabs = true,
                loadSubAgents = true,
                loadAgents = true,
                userid=_user.U_Id
            });
            if (_user.U_Is_Admin == false)
            {
                response.loadedData.agents = response.loadedData.agents.Where(a => a.LK_ID == _user.U_Id).ToList();
                response.loadedData.subagents = response.loadedData.usersHierarchy.Where(a => a.LK_ID != _user.U_Id).ToList();
            }
            return View(response);
        }
      
        public ActionResult Benefits()
        {

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                userid=_user.U_Id
                //,loadProductionTabs = true
            });
            return View(response);
        }
        public ActionResult Beneficiaries()
        {

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadProductionTabs = true,
                loadSubAgents = true,
                loadAgents = true,
              userid = _user.U_Id
            });
            if (_user.U_Is_Admin == false)
            {
                response.loadedData.agents = response.loadedData.agents.Where(a => a.LK_ID == _user.U_Id).ToList();
                response.loadedData.subagents = response.loadedData.usersHierarchy.Where(a => a.LK_ID != _user.U_Id).ToList();
            }
            return View(response);
        }
        public ActionResult Currencies()
        {

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadCurrencyRate = true
            });
            return View(response);
        }
        public ActionResult Tariff()
        {

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadPackages = true,
                loadPlans = true,
                loadProducts = true,
                loadAssignedUsers=true
            });
            return View(response);
        }

        [HttpPost]
        public IActionResult GenerateProduction(productionReport req)
        {
           
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateProduction(req, _user.U_Id);
            DataTable dataTable = ConvertToDataTable(result.reportData);
            return ExporttoExcel(dataTable, "Production");
        }
        [HttpPost]
        public IActionResult GenerateBenefits(int userid /*,DateTime datefrom, DateTime dateto*/)
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateBenefits(userid);
            DataTable dataTable = ConvertToDataTable(result.reportData);
            return ExporttoExcel(dataTable, "Production");
        }

        [HttpPost]
        public IActionResult GenerateBeneficiaries(productionReport req)
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateBeneficiaries(_user.U_Id, req);
            DataTable dataTable = ConvertToDataTable(result.reportData);
            return ExporttoExcel(dataTable, "Production");
        }

        [HttpPost]
        public IActionResult GenerateCurrencies(int req)
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateCurrencies(_user.U_Id,req);
            DataTable dataTable = ConvertToDataTable(result.reportData);
            return ExporttoExcel(dataTable, "Production");
        } 
        [HttpPost]
        public IActionResult GenerateTariff(int planid, int packageid, int assignedid, int productid)
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateTariff(_user.U_Id, packageid, planid, assignedid, productid);
            DataTable dataTable = ConvertToDataTable(result.reportData);
            return ExporttoExcel(dataTable, "Production");
        } 
        [HttpPost]
        public IActionResult GenerateManualPolicies(int batchid)
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateManualPolicies(batchid);
            DataTable dataTable = ConvertToDataTable(result.reportData);
            return ExporttoExcel(dataTable, "Production");
        }

        public LoadDataResp getChildren(int userid)
        {
            return _reportBusiness.getChildren(userid);
        }
         public LoadDataResp getProducts(int userid)
        {
            return _reportBusiness.getProducts(userid);
        }

    }
}
