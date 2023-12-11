using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class LoginResp : GlobalResponse
    {
        public dbModels.TR_Users user { get; set; }
        public string ReturnUrl { get; set; }
    }
}
