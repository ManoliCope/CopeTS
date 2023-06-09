using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.User
{
   public  class GetUserResp : GlobalResponse
    {
        public dbModels.User user { get; set; }
    }
}
