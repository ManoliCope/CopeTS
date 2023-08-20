using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Production
{
    public class ProductionPrintReq : GlobalResponse
    {
        public int PolicyID { set; get; }
    }
}
