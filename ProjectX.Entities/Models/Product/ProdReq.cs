﻿using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Product
{
    public class ProdReq
    {
    
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool is_family { get; set; }
        public DateTime activation_date { get; set; }
        public bool is_active { get; set; }
        public double is_deductible { get; set; }
        public double sports_activities { get; set; }
        public double additional_benefits { get; set; }


    }
}
