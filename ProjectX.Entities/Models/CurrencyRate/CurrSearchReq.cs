using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.CurrencyRate

{
    public class CurrSearchReq : GlobalResponse
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public int Currency_Id { get; set; }
        public double Rate { get; set; }
        public DateTime Creation_Date { get; set; }
    }
}
