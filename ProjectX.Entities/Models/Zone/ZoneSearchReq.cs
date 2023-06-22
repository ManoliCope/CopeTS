using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Zone

{
    public class ZoneSearchReq : GlobalResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<int> destinationId { get; set; }
        public List<string> destination { get; set; }



    }
}
