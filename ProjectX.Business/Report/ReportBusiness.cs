﻿using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Report;
using ProjectX.Repository.PlanRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;
using ProjectX.Repository.ReportRepository;
using ProjectX.Entities.Models.General;

namespace ProjectX.Business.Report
{
    public class ReportBusiness : IReportBusiness
    {
        IReportRepository _reportRepository;

        public ReportBusiness(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public List<dynamic> GenerateProduction(productionReport req, int userid)

        {
            
            return _reportRepository.GenerateProduction(req,userid);
        } 
        public List<dynamic> GenerateBenefits( int userid)

        {
            
            return _reportRepository.GenerateBenefits(userid);
        } 
        public List<dynamic> GenerateBeneficiaries(int userid, productionReport req)

        {
            
            return _reportRepository.GenerateBeneficiaries(userid, req);
        }
        public List<dynamic> GenerateCurrencies(int userid,int req)

        {
            
            return _reportRepository.GenerateCurrencies(userid,req);
        }
        public List<dynamic> GenerateTariff(int userid,int packageid, int planid, int assignedid, int productid)

        {
            
            return _reportRepository.GenerateTariff(userid, packageid, planid, assignedid, productid);
        }
        public List<dynamic> GenerateManualPolicies(int batchid)

        {
            
            return _reportRepository.GenerateManualPolicies(batchid);
        }
        public LoadDataResp getChildren(int userid)
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = _reportRepository.getChildren(userid);
            return response;
        }
           public LoadDataResp getProducts(int userid)
        {
            LoadDataResp response = new LoadDataResp();
            response.loadedData = _reportRepository.getProducts(userid);
            return response;
        }
        public List<dynamic> GeneratePrepaidAccounts(int U_Id,int userid, DateTime? datefrom, DateTime? dateto)
        {
            return _reportRepository.GeneratePrepaidAccounts(U_Id,userid,datefrom,dateto);
        }

    }
}
