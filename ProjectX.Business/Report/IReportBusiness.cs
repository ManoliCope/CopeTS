﻿using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.General;
using ProjectX.Entities.Models.Plan;
using ProjectX.Entities.Models.Report;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Report
{
    public interface IReportBusiness
    {
        public List<dynamic> GenerateProduction(productionReport req, int userid);
        public List<dynamic> GenerateBenefits(int userid);
        public List<dynamic> GenerateBeneficiaries(int userid, productionReport req);
        public List<dynamic> GenerateCurrencies(int userid, int req);
        public List<dynamic> GenerateManualPolicies(int batchid);
        public LoadDataResp getChildren(int userid);
        public LoadDataResp getProducts(int userid);
        public List<dynamic> GenerateTariff(int userid, int packageid, int planid, int assignedid, int productid);
        public List<dynamic> GeneratePrepaidAccounts(int U_Id, int userid, DateTime? datefrom, DateTime? dateto);

    }
}
