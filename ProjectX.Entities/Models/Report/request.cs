using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.Models.Report
{
    public class request
    {
        public string data { get; set; }
        public string filename { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string payline { get; set; }
    }
}
