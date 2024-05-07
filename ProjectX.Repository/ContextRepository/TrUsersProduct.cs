using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrUsersProduct
    {
        public int UpId { get; set; }
        public int PrId { get; set; }
        public int? UId { get; set; }
        public double? UpIssuingFees { get; set; }
        public DateTime? UpCreationDate { get; set; }
        public string? UpUploadedFile { get; set; }
        public bool? UpActive { get; set; }
    }
}
