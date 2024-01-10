using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Production
{
    public class ProductionSearchReq : GlobalResponse
    {
        public int status { get; set; }
        public int agentid { get; set; }
        public string Reference { get; set; }
        public string Beneficiarys { get; set; }
        public string Passportno { get; set; }

    }
}
