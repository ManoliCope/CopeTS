using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrPolicyDestination
    {
        public int Id { get; set; }
        public int? PolicyId { get; set; }
        public int? DestinationId { get; set; }

        public virtual TrPolicyHeader? Policy { get; set; }
    }
}
