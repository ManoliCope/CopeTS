using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace ProjectX.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void GenerateReport(dynamic req)
        {
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(req.data, (typeof(DataTable)));
            ExporttoExcel(dt);
        }
        public IActionResult ExporttoExcel(DataTable dt)
        {
            var content = new StringBuilder();
            content.AppendLine(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            content.AppendLine("<html><body>");
            content.AppendLine("<font style='font-size:10.0pt; font-family:Calibri;'>");
            content.AppendLine("<BR><BR><BR>");
            content.AppendLine("<Table border='1' bgColor='#ffffff' " +
                                 "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                                 "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

            int columnscount = dt.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {
                content.AppendLine("<Td style='background:#16478e !important;color:white !important; text-align:center'>");
                content.AppendLine("<B>");
                content.AppendLine(dt.Columns[j].Caption.ToString());
                content.AppendLine("</B>");
                content.AppendLine("</Td>");
            }
            content.AppendLine("</TR>");

            foreach (DataRow row in dt.Rows)
            {
                content.AppendLine("<TR>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    content.AppendLine("<Td>");
                    content.AppendLine(row[i].ToString());
                    content.AppendLine("</Td>");
                }
                content.AppendLine("</TR>");
            }

            content.AppendLine("</Table>");
            content.AppendLine("</font>");
            content.AppendLine("</body></html>");

            var bytes = Encoding.UTF8.GetBytes(content.ToString());
            return File(bytes, "application/ms-excel", "Report.xls");
        }
    
}
}
