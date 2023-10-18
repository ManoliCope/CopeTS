using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Plan;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Report
{
    public interface IReportBusiness
    {
        public List<dynamic> GenerateProduction(int req, int userid, DateTime datefrom, DateTime dateto);
        public List<dynamic> GenerateBenefits(int userid, DateTime datefrom, DateTime dateto);
        public List<dynamic> GenerateBeneficiaries(int userid, DateTime datefrom, DateTime dateto);
        public List<dynamic> GenerateCurrencies(int userid);
    }
}
