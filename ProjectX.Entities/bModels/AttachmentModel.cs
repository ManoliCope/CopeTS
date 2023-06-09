using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.bModels
{
    public class AttachmentModel
    {
        public int IdFileDirectory { get; set; }
        public string FileDirectory { get; set; }
        public List<RequiredDocument> requiredDocuments { get; set; } = new List<RequiredDocument>();
        public List<Attachment> attahcments { get; set; } = new List<Attachment>();
        public bool CanUpload { get; set; } = true;
    }
}