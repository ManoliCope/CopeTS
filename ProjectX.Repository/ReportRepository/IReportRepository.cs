using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Report;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.ReportRepository
{
    public interface IReportRepository
    {
        public List<dynamic> GenerateProduction(productionReport req, int userid);
        public List<dynamic> GenerateBenefits(int userid);
        public List<dynamic> GenerateBeneficiaries(int userid, productionReport req);
        public List<dynamic> GenerateCurrencies(int userid);

    }
}
