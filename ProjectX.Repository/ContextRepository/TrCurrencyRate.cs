using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrCurrencyRate
    {
        public TrCurrencyRate()
        {
            TrUsers = new HashSet<TrUser>();
        }

        public int CrId { get; set; }
        public int? CrCurrencyId { get; set; }
        public double? CrRate { get; set; }
        public DateTime? CrCreationDate { get; set; }

        public virtual TrCurrency? CrCurrency { get; set; }
        public virtual ICollection<TrUser> TrUsers { get; set; }
    }
}
