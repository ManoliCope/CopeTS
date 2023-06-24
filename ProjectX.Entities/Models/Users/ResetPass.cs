using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class ResetPass : GlobalResponse
    {
        public int userId { get; set; }
        public string oldPass { get; set; }
        public string newPass { get; set; }
        public string conPass { get; set; }
    }
}
