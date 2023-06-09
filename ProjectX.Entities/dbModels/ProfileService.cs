using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class ProfileService
    {
        public int IdProfileType { get; set; }
        public string ProfileType { get; set; }
        public int IdCaseType { get; set; }
        public string CaseType { get; set; }
    }
}
