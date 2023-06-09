using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Report
{
    public class GetReportResp : GlobalResponse
    {
        public LoadDataModel loadedData { get; set; } = new LoadDataModel();
        public List<dynamic> reportData { get; set; }
    }
}
