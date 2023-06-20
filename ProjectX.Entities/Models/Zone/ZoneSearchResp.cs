using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Zone
{
    public class ZoneSearchResp : GlobalResponse
    {
        public List<TR_Zone> zone { get; set; }
    }

}
