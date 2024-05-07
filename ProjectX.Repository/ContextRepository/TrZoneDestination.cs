using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrZoneDestination
    {
        public int ZdId { get; set; }
        public int? ZId { get; set; }
        public int? DId { get; set; }

        public virtual TrDestination? DIdNavigation { get; set; }
        public virtual TrZone? ZIdNavigation { get; set; }
    }
}
