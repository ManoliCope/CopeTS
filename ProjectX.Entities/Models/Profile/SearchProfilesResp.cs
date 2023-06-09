using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Profile
{
    public class SearchProfilesResp : GlobalResponse
    {
        public List<dbModels.Profile> profiles { get; set; }
    }
}
