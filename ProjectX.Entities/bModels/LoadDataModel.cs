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
        public List<LookUpp> sex { get; set; }
        public List<LookUpp> format { get; set; }
        public List<Destination> destinations { get; set; }
        public List<LookUpp> userCategory { get; set; }
        public List<LookUpp> roundingRule { get; set; }
        public List<LookUpp> superAgents { get; set; }
        public List<LookUpp> currencies { get; set; }
        public List<LookUpp> currencyRate { get; set; }
        public List<LookUpp> benefitTitle { get; set; }
        public List<LookUpp> productionTabs { get; set; }
        
    }
}