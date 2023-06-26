using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.bModels
{
    public class Tariff
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



    }
}
