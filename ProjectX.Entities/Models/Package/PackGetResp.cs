using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Package
{
    public class PackGetResp : GlobalResponse
    {
        public dbModels.TR_Package package { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
