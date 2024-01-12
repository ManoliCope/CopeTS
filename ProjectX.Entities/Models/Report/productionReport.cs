using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Entities.Models.Report
{
    public class productionReport
    {
        //var request = $("#prodId").val();
        //var datefrom = $("#datefrom").val();
        //var dateto = $("#dateto").val();
        //var agentId = $("#agentId").val();
        //var subAgentId = $("#subAgentId").val();
        //var policyStatus = $("#policyStatus").val();
        //var policyNumber = $("#policyNumber").val();
        //var clientFirstName = $("#clientFirstName").val();
        //var clientLastName = $("#clientLastName").val();
        //var passportNumber = $("#passportNumber").val();
        public int prodId { get; set; }
        public DateTime? datefrom { get; set; }
        public DateTime? dateto { get; set; }
        public int agentId { get; set; }
        public int subAgentId { get; set; }
        public int policyStatus { get; set; }
        public int policyNumber { get; set; }
        public string clientFirstName { get; set; }
        public string clientLastName { get; set; }
        public string passportNumber { get; set; }
    }
}
