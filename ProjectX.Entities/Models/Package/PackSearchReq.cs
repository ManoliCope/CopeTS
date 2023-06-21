using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Package

{
    public class PackSearchReq : GlobalResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public int cobId { get; set; }
        public int zoneId { get; set; }
        public int remove_deductable { get; set; }
        public int adult_no { get; set; }
        public int children_no { get; set; }
        public bool pa_included { get; set; }
        


    }
}
