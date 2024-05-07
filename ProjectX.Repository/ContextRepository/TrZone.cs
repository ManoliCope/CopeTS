using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrZone
    {
        public TrZone()
        {
            TrPackages = new HashSet<TrPackage>();
            TrZoneDestinations = new HashSet<TrZoneDestination>();
        }

        public int ZId { get; set; }
        public string? ZTitle { get; set; }

        public virtual ICollection<TrPackage> TrPackages { get; set; }
        public virtual ICollection<TrZoneDestination> TrZoneDestinations { get; set; }
    }
}
