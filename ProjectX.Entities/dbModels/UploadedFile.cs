using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class UploadedFile
    {
        [JsonProperty(PropertyName = "idAttachment")]
        public int idAttachment { get; set; }
        [JsonProperty(PropertyName = "reference")]
        public string IdReference { get; set; }
        [JsonProperty(PropertyName = "fileName")]
        public string FileName { get; set; }
        [JsonProperty(PropertyName = "fileDesc")]
        public string FileDesc { get; set; }
        [JsonProperty(PropertyName = "idFileType")]
        public int IdFileType { get; set; }
        [JsonProperty(PropertyName = "isDone")]
        public bool IsDone { get; set; }
        [JsonProperty(PropertyName = "filePath")]
        public string FilePath { get; set; }
        [JsonIgnore]
        public IFormFile file { get; set; }
        [JsonProperty(PropertyName = "idDocumentType")]
        public int idDocumentType { get; set; }
        [JsonProperty(PropertyName = "documentType")]
        public string documentType { get; set; }
    }
}
