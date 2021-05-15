using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi
{
    public interface IProductServices
    {
        public List<ProductEntity> GetProduct();
       
    }
}
