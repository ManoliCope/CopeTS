using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPolicyAdditionalBenefit
    {
        public int Id { get; set; }
        public int? PolicyId { get; set; }
        public int? AbId { get; set; }
        public int? Insuredid { get; set; }
        public decimal? Price { get; set; }

        public virtual TrPolicyHeader? Policy { get; set; }
    }
}
