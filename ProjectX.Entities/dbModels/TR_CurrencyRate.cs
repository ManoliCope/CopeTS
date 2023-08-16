using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.dbModels
{
    public class TR_CurrencyRate
    {
        public int CR_Id { get; set; }
        public int CR_Currency_Id { get; set; }
        public string CR_Currency { get; set; }
        public double CR_Rate { get; set; }
        public DateTime CR_Creation_Date { get; set; }
    }
}
