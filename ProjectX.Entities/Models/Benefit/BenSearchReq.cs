using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Benefit

{
    public class BenSearchReq : GlobalResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public double limit { get; set; }
        public int packageId { get; set; }
        public bool is_Plus { get; set; }




    }
}
