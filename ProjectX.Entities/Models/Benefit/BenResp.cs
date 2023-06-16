﻿using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Benefit
{
    public class BenResp : GlobalResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public float limit { get; set; }
    }
}