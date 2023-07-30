using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Production
{
    //public class ProductionReq
    //{
    //    public List<insuredQuotation> InsuredQuotations { get; set; }
    //}

    public class ProductionReq
    {
        public int Insured { get; set; }
        public int Ages { get; set; }
        public int Product { get; set; }
        public int Zone { get; set; }
        public int Durations { get; set; }
    }
}
