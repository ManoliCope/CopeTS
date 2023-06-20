using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Package
{
    public class PackSearchResp : GlobalResponse
    {
        public List<TR_Package> package { get; set; }
    }

}
