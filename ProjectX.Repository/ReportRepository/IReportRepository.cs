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
        public List<dynamic> GenerateProduction(int req, int userid, DateTime datefrom, DateTime dateto);
        public List<dynamic> GenerateBenefits(int userid, DateTime datefrom, DateTime dateto);
        public List<dynamic> GenerateBeneficiaries(int userid, DateTime datefrom, DateTime dateto);
        public List<dynamic> GenerateCurrencies(int userid);

    }
}
