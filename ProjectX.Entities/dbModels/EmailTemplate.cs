using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class EmailTemplate
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool WithAttachment { get; set; }
        public string recepients { get; set; }
        public string LOB { get; set; }

    }
}
