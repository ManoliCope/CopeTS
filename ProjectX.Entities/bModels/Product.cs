using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.bModels
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool is_family { get; set; }
        public DateTime activation_date { get; set; }
        public bool is_active { get; set; }
        public float is_deductible { get; set; }
        public float sports_activities { get; set; }
        public float additional_benefits { get; set; }

    }
}
