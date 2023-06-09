using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.dbModels
{
    public class FileDirectory
    {
        public int IdFileDirectory { get; set; }
        public int IdFileSection { get; set; }
        public string Name { get; set; }
        public string NameMd5 { get; set; }
        public string Description { get; set; }
        public int IdFileType { get; set; }
        public string UploadDirectory { get; set; }
        public string ApiLink { get; set; }
        public string SecondUploadDirectory { get; set; }
        public int IdObjectReference { get; set; }
    }
}
