using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Users
{
    public class UsProReq
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? UsersId { get; set; }
        public double? IssuingFees { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Action { get; set; }
        public string UploadedFile { get; set; }
        public string UploadedFolder { get; set; }
    }
}
