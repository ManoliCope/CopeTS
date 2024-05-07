using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPrepaidAccount
    {
        public TrPrepaidAccount()
        {
            TrPrepaidAccountsTransactions = new HashSet<TrPrepaidAccountsTransaction>();
        }

        public int PaId { get; set; }
        public int UId { get; set; }
        public double? PaAmount { get; set; }
        public bool? PaActive { get; set; }
        public DateTime? PaCreationDate { get; set; }
        public DateTime? PaModifiactionDate { get; set; }

        public virtual TrUser UIdNavigation { get; set; } = null!;
        public virtual ICollection<TrPrepaidAccountsTransaction> TrPrepaidAccountsTransactions { get; set; }
    }
}
