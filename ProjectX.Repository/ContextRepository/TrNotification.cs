using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrNotification
    {
        public int NId { get; set; }
        public string? NTitle { get; set; }
        public string? NText { get; set; }
        public string? NCreatedBy { get; set; }
        public DateTime? NCreationDate { get; set; }
        public DateTime? NExpiryDate { get; set; }
        public TimeSpan? NExpiryTime { get; set; }
        public bool? NIsActive { get; set; }
        public bool? NIsDeleted { get; set; }
        public bool? NIsImportant { get; set; }
    }
}
