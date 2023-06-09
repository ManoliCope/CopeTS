using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public List<int> destinationId { get; set; }
        public List<string> destination { get; set; }
    }
}
