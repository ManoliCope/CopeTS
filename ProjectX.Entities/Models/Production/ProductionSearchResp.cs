using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Package
{
    public class ProductionSearchResp : GlobalResponse
    {
        public List<TR_PolicyHeader> Production { get; set; }
    }

}
