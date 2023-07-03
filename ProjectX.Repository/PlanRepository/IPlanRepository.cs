using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.PlanRepository
{
    public interface IPlanRepository
    {
        public PlanResp ModifyPlan(PlanReq req, string act, int userid);
        public List<TR_Plan> GetPlanList(PlanSearchReq req);
        public TR_Plan GetPlan(int IdPlan);
    }
}
