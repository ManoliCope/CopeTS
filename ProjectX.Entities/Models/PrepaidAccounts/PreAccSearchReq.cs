using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.PrepaidAccounts

{
    public class PreAccSearchReq : GlobalResponse
    {
        public int id { get; set; }
        public int? acc_Id { get; set; }
        public int? action { get; set; }
        public double? amount { get; set; }
        public string? details { get; set; }
        public DateTime? creationDate { get; set; }


    }
}
