using ProjectX.Repository.ProductRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Product
{
    public class ProductBusiness : IProductBusiness
    {
        IProductRepository _productRepository;

        public ProductBusiness(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}
