using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Tariff
{
    public class TariffReq 
    {
        public int id { get; set; }
        public int idPackage { get; set; }
        public string package { get; set; }
        public int start_age { get; set; }
        public int end_age { get; set; }
        public int number_of_days { get; set; }
        public float price_amount { get; set; }
        public float net_premium_amount { get; set; }
        public float pa_amount { get; set; }
        public DateTime tariff_starting_date { get; set; }
    }
}
