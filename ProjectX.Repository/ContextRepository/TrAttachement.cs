using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrAttachement
    {
        public int AtId { get; set; }
        public int FdId { get; set; }
        public int? AtIdDocumentType { get; set; }
        public int? AtIdReferenceObject { get; set; }
        public int AtIdReference { get; set; }
        public string? AtDescription { get; set; }
        public string? AtFileName { get; set; }
        public string AtNameMd5 { get; set; } = null!;
        public string AtExtension { get; set; } = null!;
        public byte AtIdFileType { get; set; }
        public DateTime AtCreated { get; set; }
        public int AtCreatedBy { get; set; }
        public bool AtIsDeleted { get; set; }
    }
}
