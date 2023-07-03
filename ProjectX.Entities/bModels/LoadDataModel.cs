using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.bModels
{
    public class LoadDataModel
    {
        public List<LookUpp> benefits { get; set; }
        public List<LookUpp> products { get; set; }
        public List<LookUpp> plans { get; set; }
        public List<LookUpp> packages { get; set; }
        public List<LookUpp> tariffs { get; set; }
        public List<LookUpp> users { get; set; }
        public List<LookUpp> zones { get; set; }
        public List<LookUpp> destinations { get; set; }
        
    }
}