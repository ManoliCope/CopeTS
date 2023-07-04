using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Plan;
using ProjectX.Repository.PlanRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;

namespace ProjectX.Business.Plan
{
    public class PlanBusiness : IPlanBusiness
    {
        IPlanRepository _planRepository;

        public PlanBusiness(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }
        public PlanResp ModifyPlan(PlanReq req, string act, int userid)
        {
            PlanResp response = new PlanResp();
            response = _planRepository.ModifyPlan(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Plan");
            return response;
           
        }
        public List<TR_Plan> GetPlanList(PlanSearchReq req)
        {
            return _planRepository.GetPlanList(req);
        }
        public PlanResp GetPlan(int IdPlan)
        {
            TR_Plan repores = _planRepository.GetPlan(IdPlan);
            PlanResp resp = new PlanResp();
            resp.id = repores.PL_Id;
            resp.title = repores.PL_Title;
           

            return resp;
        }
    }
}
