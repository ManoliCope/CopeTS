using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.General
{
    public class LoadDataResp : GlobalResponse
    {
        public LoadDataModel loadedData { get; set; }
    }
}
