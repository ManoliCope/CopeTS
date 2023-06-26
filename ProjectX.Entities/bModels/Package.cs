﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.bModels
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int ZoneID { get; set; }
        public int Remove_deductable { get; set; }
        public int Adult_No { get; set; }
        public int Children_No { get; set; }
        public bool PA_Included { get; set; }
    }
}
