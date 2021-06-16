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

            var UserBuilder = builder.Entity<UserEntity>();
            UserBuilder.ToTable("Users", "dbo");
            UserBuilder.HasKey(x => x.UsersID);
            UserBuilder.Property(x => x.UsersID).ValueGeneratedOnAdd();

            var MembershipBuilder = builder.Entity<WebpagesMembershipEntity>();
            MembershipBuilder.ToTable("webpages_Membership", "dbo");
            MembershipBuilder.HasKey(x => x.UserId);
        }

        public DbSet<ProductEntity> productEntities { get; set; }
        public DbSet<StockEntity> stockEntities { get; set; }
        public DbSet<UserEntity> userEntities { get; set; }
        public DbSet<WebpagesMembershipEntity> membershipEntities { get; set; }
    }
}
