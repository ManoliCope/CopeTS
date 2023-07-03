using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Plan
{
    public interface IPlanBusiness
    {
        public PlanResp ModifyPlan(PlanReq req, string act, int userid);
        public List<TR_Plan> GetPlanList(PlanSearchReq req);
        public PlanResp GetPlan(int IdPlan);
    }
}
