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
        public List<dynamic> GenerateCurrencies(int userid, int req);
        public List<dynamic> GenerateManualPolicies(int batchid);
        public LoadDataModel getChildren(int userid);
        public LoadDataModel getProducts(int userid);
        public List<dynamic> GenerateTariff(int userid, int packageid, int planid, int assignedid, int productid);
    }
}
