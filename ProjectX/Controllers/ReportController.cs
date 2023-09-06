﻿using Microsoft.AspNetCore.Mvc;
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

        public ReportController(IHttpContextAccessor httpContextAccessor, IOptions<TrAppSettings> appIdentitySettingsAccessor, IGeneralBusiness generalBusiness, IReportBusiness reportBusiness, IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _reportBusiness = reportBusiness;
            _generalBusiness = generalBusiness;
            _appSettings = appIdentitySettingsAccessor.Value;
            _user = (TR_Users)httpContextAccessor.HttpContext.Items["User"];
            _env = env;

        }
        public IActionResult Index()
        {
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
                loadProductionTabs = true
            });
            return View(response);
        }
      
        public ActionResult Benefits()
        {

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadProductionTabs = true
            });
            return View(response);
        }
        public ActionResult Beneficiaries()
        {

            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadProductionTabs = true
            });
            return View(response);
        }

        [HttpPost]
        public IActionResult GenerateProduction(int req)
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateProduction(req, _user.U_Id);
            DataTable dataTable = ConvertToDataTable(result.reportData);
            return ExporttoExcel(dataTable, "Production");
        }
        [HttpPost]
        public IActionResult GenerateBenefits()
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateBenefits(_user.U_Id);
            DataTable dataTable = ConvertToDataTable(result.reportData);
            return ExporttoExcel(dataTable, "Production");
        }

        [HttpPost]
        public IActionResult GenerateBeneficiaries()
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateBeneficiaries(_user.U_Id);
            DataTable dataTable = ConvertToDataTable(result.reportData);
            return ExporttoExcel(dataTable, "Production");
        }
      
    }
}