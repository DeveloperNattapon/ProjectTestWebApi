using Microsoft.EntityFrameworkCore;

namespace ProjectTest.Models
{
    public class DBtestContext : DbContext 
    {
        public DBtestContext(DbContextOptions<DBtestContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            var ProductBuilder = builder.Entity<ProductEntity>();
            ProductBuilder.ToTable("Persons", "dbo");
            ProductBuilder.HasKey(x => x.PersonsId);

            var StockBuilder = builder.Entity<StockEntity>();
            StockBuilder.ToTable("Stock", "dbo");
            StockBuilder.HasKey(x => x.StockId);
        }

        public DbSet<ProductEntity> productEntities { get; set; }
        public DbSet<StockEntity> stockEntities { get; set; }
    }
}
