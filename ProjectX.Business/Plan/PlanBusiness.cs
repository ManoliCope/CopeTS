using ProjectX.Repository.PlanRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Plan
{
    public class PlanBusiness : IPlanBusiness
    {
        IPlanRepository _planRepository;

        public PlanBusiness(IPlanRepository PlanRepository)
        {
            _planRepository = PlanRepository;
        }
    }
}
