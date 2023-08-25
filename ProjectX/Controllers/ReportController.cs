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
        [HttpPost]
        public void GenerateReport(request req)
        {
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(req.data, (typeof(DataTable)));
            ExporttoExcel(dt,req.filename);
        }
        //public IActionResult ExporttoExcel(DataTable dt,string filename)
        //{
        //    var content = new StringBuilder();
        //    content.AppendLine(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        //    content.AppendLine("<html><body>");
        //    content.AppendLine("<font style='font-size:10.0pt; font-family:Calibri;'>");
        //    content.AppendLine("<BR><BR><BR>");
        //    content.AppendLine("<Table border='1' bgColor='#ffffff' " +
        //                         "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
        //                         "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

        //    int columnscount = dt.Columns.Count;

        //    for (int j = 0; j < columnscount; j++)
        //    {
        //        content.AppendLine("<Td style='background:#16478e !important;color:white !important; text-align:center'>");
        //        content.AppendLine("<B>");
        //        content.AppendLine(dt.Columns[j].Caption.ToString());
        //        content.AppendLine("</B>");
        //        content.AppendLine("</Td>");
        //    }
        //    content.AppendLine("</TR>");

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        content.AppendLine("<TR>");
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            content.AppendLine("<Td>");
        //            content.AppendLine(row[i].ToString());
        //            content.AppendLine("</Td>");
        //        }
        //        content.AppendLine("</TR>");
        //    }

        //    content.AppendLine("</Table>");
        //    content.AppendLine("</font>");
        //    content.AppendLine("</body></html>");

        //    var bytes = Encoding.UTF8.GetBytes(content.ToString());
        //    return PhysicalFile(bytes, "application/ms-excel", filename);
        //}
        public ActionResult Production()
        {
            
            LoadDataResp response = _generalBusiness.loadData(new Entities.bModels.LoadDataModelSetup
            {
                loadProductionTabs = true
            });
            return View(response);
        }
        [HttpPost]
        public GetReportResp GenerateProduction(int req)
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateProduction(req,_user.U_Id);
            result.statusCode.code = 1;
            return result;
        }
        public IActionResult ExporttoExcel(DataTable dt, string filename)
        {

            var sb = new StringBuilder();
            sb.AppendLine("<table border='1' style='border-collapse: collapse;'>");

            // Add header row
            sb.AppendLine("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sb.AppendLine($"<th style='background-color: #16478e; color: white; text-align: center;'>{column.ColumnName}</th>");
            }
            sb.AppendLine("</tr>");

            // Add data rows
            foreach (DataRow row in dt.Rows)
            {
                sb.AppendLine("<tr>");
                foreach (var item in row.ItemArray)
                {
                    sb.AppendLine($"<td>{item}</td>");
                }
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");

            var content = sb.ToString();
            var bytes = Encoding.UTF8.GetBytes(content);

            using (var memoryStream = new MemoryStream())
            {
                // Create a memory stream with the Excel content
                memoryStream.Write(bytes, 0, bytes.Length);

                // Return the memory stream as a downloadable file
                
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
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
        public GetReportResp GenerateBenefits()
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateBenefits(_user.U_Id);
            result.statusCode.code = 1;
            return result;
        }
        [HttpPost]
        public GetReportResp GenerateBeneficiaries()
        {
            GetReportResp result = new GetReportResp();
            result.reportData = _reportBusiness.GenerateBeneficiaries( _user.U_Id);
            result.statusCode.code = 1;
            return result;
        }
    }
}
