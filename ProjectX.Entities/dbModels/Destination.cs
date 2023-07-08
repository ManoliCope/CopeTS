using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.dbModels
{
    public class Destination
    {
        public int D_Id { get; set; }
        public string D_Destination { get; set; }
        public int D_IdContinent { get; set; }
        public string D_Continent { get; set; }
    }
}
