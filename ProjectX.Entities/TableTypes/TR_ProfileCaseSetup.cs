using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.TableTypes
{
    public class TR_ProfileCaseSetup
    {
        public Guid? IdProfileCaseSetup { get; set; }
        public int? IdFeesType { get; set; }
        public int? IdCaseComplexity { get; set; }
        public int? IdCountry { get; set; }
        public int? IdCurrency { get; set; }
        public decimal? Amount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}
