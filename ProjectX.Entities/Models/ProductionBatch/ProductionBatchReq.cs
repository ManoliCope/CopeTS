using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.ProductionBatch
{
    public class ProductionBatchReq 
    {
        public int id { get; set; }
        public string name { get; set; }
        public string filename { get; set; }
        public int userid { get; set; }


    }
}
