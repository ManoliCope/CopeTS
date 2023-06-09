using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class ProfileContract
    {
        public int IdContract { get; set; }
        public int IdProfile { get; set; }
        public string Profile { get; set; }
        public int IdProduct { get; set; }
        public string Product { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}
