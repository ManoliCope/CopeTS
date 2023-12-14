using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.ProductionBatch
{
    public class ProductionBatchDetailsReq 
    {
        public int BatchID { get; set; }
        public string ReferenceNumber { get; set; }
        public string Type { get; set; }
        public string Plan { get; set; }
        public string Zone { get; set; }
        public int Days { get; set; }
        public DateTime StartDate { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string PassportNumber { get; set; }
        public string Nationality { get; set; }
        public decimal PremiumInUSD { get; set; }
        public decimal NetInUSD { get; set; }
        public bool isError { get; set; }
    }
}
