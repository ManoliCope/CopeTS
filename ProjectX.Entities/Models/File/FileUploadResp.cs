using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.File
{
    public class FileUploadResp : GlobalResponse
    {
        public List<UploadedFile> uploadedFiles { get; set; } = new List<UploadedFile>();
    }
}
