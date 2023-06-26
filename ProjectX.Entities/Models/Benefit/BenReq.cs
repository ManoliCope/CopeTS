using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Benefit
{
    public class BenReq
    {
        public int id { get; set; }
        public string title { get; set; }
        public double limit { get; set; }
    }
}
