using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.dbModels
{
    public class TR_PolicyHeader
    {
        public int PolicyID { get; set; }
        public string Reference { get; set; }
        public int Duration { get; set; }
        public DateTime Todate { get; set; }
        public DateTime Fromdate { get; set; }
        public int ProductID { get; set; }
        public int ZoneID { get; set; }
        public bool IsIndividual { get; set; }
        public bool IsGroup { get; set; }
        public bool IsFamily { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool IsCanceled { get; set; }
        public decimal InitialPremium { get; set; }
        public decimal AdditionalValue { get; set; }
        public decimal TaxVATValue { get; set; }
        public decimal StampsValue { get; set; }
        public decimal GrandTotal { get; set; }

        public string mainname { get; set; }
        public string nbofclients { get; set; }
        public bool IsEditable { get; set; }
        public int Status { get; set; }

    }

}
