using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class TR_Zone
    {
        public int Z_Id { get; set; }
        public string Z_Title { get; set; }
        public List<int> Z_Destination_Id { get; set; }
       
    }
}
