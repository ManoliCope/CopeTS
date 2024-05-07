using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPrepaidAccountsTransaction
    {
        public int PatId { get; set; }
        public int? PaId { get; set; }
        public int? PatAction { get; set; }
        public double? PatAmount { get; set; }
        public string? PatDetails { get; set; }
        public DateTime? PatCreationDate { get; set; }
        public int? PatCreatedBy { get; set; }
        public int? PolicyId { get; set; }

        public virtual TrPrepaidAccount? Pa { get; set; }
    }
}
