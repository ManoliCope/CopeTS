using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Tariff

{
    public class TariffSearchReq : GlobalResponse
    {
        public int id { get; set; }
        public int idPackage { get; set; }
        public string package { get; set; }
        public int start_age { get; set; }
        public int end_age { get; set; }
        public int number_of_days { get; set; }
        public double price_amount { get; set; }
        public double net_premium_amount { get; set; }
        public double pa_amount { get; set; }
        public DateTime tariff_starting_date { get; set; }
        public double Override_Amount { get; set; }
        public int planId { get; set; }
    }
}
