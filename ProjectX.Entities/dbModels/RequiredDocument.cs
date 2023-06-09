using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class RequiredDocument
    {
        public int IdDocumentType { get; set; }
        public string DocumentType { get; set; }
        public bool IsMandatory { get; set; }
    }
}