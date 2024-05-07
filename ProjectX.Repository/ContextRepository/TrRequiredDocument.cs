using System;
using System.Collections.Generic;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TrRequiredDocument
    {
        public int RdId { get; set; }
        public byte RdIdReferenceObject { get; set; }
        public short FdId { get; set; }
        public byte RdIdDocumentType { get; set; }
        public bool RdIsMandatory { get; set; }
        public bool RdIsUnique { get; set; }
        public bool RdIsHtml { get; set; }
    }
}
