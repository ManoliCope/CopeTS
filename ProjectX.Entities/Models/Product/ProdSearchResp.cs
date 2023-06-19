using ProjectX.Entities.dbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities.Models.Product
{
    public class ProdSearchResp : GlobalResponse
    {
        public List<TR_Product> products { get; set; }
    }

}
