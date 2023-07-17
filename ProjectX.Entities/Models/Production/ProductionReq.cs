using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Production
{
    public class ProductionReq
    {
    
        public int Zone { get; set; }
        public int Product { get; set; }
        public List<int> Ages {get; set; }
        public List<int> Durations { get; set; }
    }
}
