using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectX.Entities.Models.File
{
    public class ClearFileReq
    {
        [JsonProperty(PropertyName = "idAttachment")]
        [Required]
        public int idAttachment { get; set; }
        [JsonProperty(PropertyName = "Section")]
        [Required]
        public int IdFileDirectory { get; set; }
        [JsonProperty(PropertyName = "Body")]
        [Required]
        public string IdReference { get; set; }
        [JsonProperty(PropertyName = "idDocumentType")]
        [Required]
        public int idDocumentType { get; set; }
    }
}
