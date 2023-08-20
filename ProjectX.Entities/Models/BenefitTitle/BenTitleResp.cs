using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.BenefitTitle
{
    public class BenTitleResp : GlobalResponse
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}
