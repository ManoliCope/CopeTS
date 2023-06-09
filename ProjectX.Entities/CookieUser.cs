using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProjectX.Entities
{
    public class CookieUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserFullName { get; set; }
        [JsonIgnore]
        public string refreshedtoken { get; set; }
    }
}
