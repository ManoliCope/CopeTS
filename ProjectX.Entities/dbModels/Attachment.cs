using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class Attachment
    {
        public int IdAttachment { get; set; }
        [JsonIgnore]
        public string IdReference { get; set; }
        [JsonProperty(PropertyName = "fileUrl")]
        public string FileUrl { get; set; }
        [JsonProperty(PropertyName = "thumbUrl")]
        public string ThumbUrl { get; set; }
        [JsonProperty(PropertyName = "fileName")]
        public string FileName { get; set; }
        [JsonProperty(PropertyName = "idFileType")]
        public int IdFileType { get; set; }
        [JsonProperty(PropertyName = "fileFormat")]
        public string FileFormat { get; set; }
        [JsonProperty(PropertyName = "idDocumentType")]
        public int IdDocumentType { get; set; }
        [JsonProperty(PropertyName = "documentType")]
        public string DocumentType { get; set; }
        [JsonProperty(PropertyName = "f")]
        public string f { get; set; }
        [JsonProperty(PropertyName = "r")]
        public string r { get; set; }
        [JsonProperty(PropertyName = "isDone")]
        public bool IsDone { get; set; }
        [JsonProperty(PropertyName = "idFileDirectory")]
        public int IdFileDirectory { get; set; }
    }
}