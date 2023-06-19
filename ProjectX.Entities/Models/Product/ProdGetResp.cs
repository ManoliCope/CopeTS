using ProjectX.Entities.bModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Product
{
    public class ProdGetResp : GlobalResponse
    {
        public dbModels.TR_Product product { get; set; }
        public LoadDataModel loadedData { get; set; }
    }
}
