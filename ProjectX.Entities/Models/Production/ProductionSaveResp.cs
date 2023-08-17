using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Production

{
    public class ProductionSaveResp : GlobalResponse
    {
        public int PolicyID { set; get; }
    }
}
