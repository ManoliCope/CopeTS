using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Report
{
    public interface IReportBusiness
    {
        public List<dynamic> GenerateProduction(int req, int userid);
        public List<dynamic> GenerateBenefits(int userid);
        public List<dynamic> GenerateBeneficiaries(int userid);

    }
}
