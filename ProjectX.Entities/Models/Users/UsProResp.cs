using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class UsProResp : GlobalResponse
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? UsersId { get; set; }
        public double? IssuingFees { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
