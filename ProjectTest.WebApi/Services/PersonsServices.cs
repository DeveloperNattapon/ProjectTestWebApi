using ProjectTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi
{
    public class PersonsServices : IProductServices
    {
        private readonly DBtestContext _context;

        public PersonsServices(DBtestContext context) 
        {
            _context = context;
        }

        public List<ProductEntity> GetProduct()
        {
            var product = new List<ProductEntity>();
            try
            {
                product = _context.productEntities.Select(s => new ProductEntity
                {
                    PersonsId  = s.PersonsId,
                    PersonsName = s.PersonsName,
                    imageUrl = s.imageUrl,
                    price = s.price

                }).ToList();
            }
            catch (Exception ex)
            {
                throw new NullReferenceException(ex.Message);
            }

            return product;
        }
    }
}
