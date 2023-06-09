using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class Group
    {
        public int GR_ID { get; set; }
        public string GR_Name { get; set; }
        public bool GR_IsAdmin { get; set; }
        public bool GR_IsActive { get; set; }
        public List<User> users { get; set; }
        public List<Page> pages { get; set; }
    }
}
