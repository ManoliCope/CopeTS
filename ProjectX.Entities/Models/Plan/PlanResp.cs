using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Plan
{
    public class PlanResp : GlobalResponse
    {
        public int id { get; set; }
        public string title { get; set; }
    }
}
