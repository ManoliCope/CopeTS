using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProjectX.Entities.dbModels
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserFullName { get; set; }
        public bool SendEmail { get; set; }
        public string Email { get; set; }
        public int IdProfile { get; set; }
        public string Profile { get; set; }
        public Group group { get; set; }
        //[JsonIgnore]
        //public string refreshedtoken { get; set; }
    }
}
