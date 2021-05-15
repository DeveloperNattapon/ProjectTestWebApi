using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectTest.Models
{
    public class DummyData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DBtestContext>();
                context.Database.EnsureCreated();
                //context.Database.Migrate();

                // Look for any ailments
                if (context.productEntities != null && context.productEntities.Any())
                    return;   // DB has already been seeded

                var product = GetProduct().ToArray();
                context.productEntities.AddRange(product);
                context.SaveChanges();

                //var medications = GetMedications().ToArray();
                //context.Medications.AddRange(medications);
                //context.SaveChanges();

               
            }
        }

        public static List<ProductEntity> GetProduct()
        {

            List<ProductEntity> products = new List<ProductEntity>
            {
               // new ProductEntity { id = 1 , name = "" ,imageUrl = "" , price = 0.0}
            };

            return products;
        }
    }
}
