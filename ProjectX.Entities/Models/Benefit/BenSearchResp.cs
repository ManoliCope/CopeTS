using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Benefit
{
    public class BenSearchResp : GlobalResponse
    {
        public List<TR_Benefit> benefit { get; set; }
    }

}
