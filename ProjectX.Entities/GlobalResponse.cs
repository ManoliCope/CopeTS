using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProjectX.Entities
{
    public class GlobalResponse
    {
        public StatusCode statusCode { get; set; } = new StatusCode();
    }
    public class StatusCode
    {
        public int code { get; set; } = 0;
        public string message { get; set; }
        [JsonIgnore]
        public string idLanguage { get; set; } = "1";
    }
}
