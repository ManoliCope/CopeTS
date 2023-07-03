using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Plan
{
    public class PlanSearchResp : GlobalResponse
    {
        public List<TR_Plan> plan { get; set; }
    }

}
