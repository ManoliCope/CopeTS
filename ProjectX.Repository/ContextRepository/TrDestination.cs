using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrDestination
    {
        public TrDestination()
        {
            TrZoneDestinations = new HashSet<TrZoneDestination>();
        }

        public int DId { get; set; }
        public string? DDestination { get; set; }
        public int? DIdContinent { get; set; }
        public string? DContinent { get; set; }

        public virtual ICollection<TrZoneDestination> TrZoneDestinations { get; set; }
    }
}
