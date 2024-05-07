using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrCurrency
    {
        public TrCurrency()
        {
            TrCurrencyRates = new HashSet<TrCurrencyRate>();
        }

        public int CuId { get; set; }
        public string CuName { get; set; } = null!;
        public string? CuSymbol { get; set; }
        public string? CuCode { get; set; }
        public byte CuFractionNo { get; set; }
        public bool? CuIsActive { get; set; }

        public virtual ICollection<TrCurrencyRate> TrCurrencyRates { get; set; }
    }
}
