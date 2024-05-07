using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrNotificationsLog
    {
        public int NlId { get; set; }
        public int? NId { get; set; }
        public string? NlActionType { get; set; }
        public DateTime? NlActionDate { get; set; }
        public string? NlName { get; set; }
    }
}
