using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Production
{
    public interface IProductionBusiness
    {
        public List<TR_Product> GetProductsByType(int id);
        public List<TR_Destinations> GetDestinationByZone(int id);



    }
}
