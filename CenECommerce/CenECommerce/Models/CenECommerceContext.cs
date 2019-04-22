namespace CenECommerce.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class CenECommerceContext : DbContext
    {
        public CenECommerceContext() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.
                Conventions.
                Remove<
                    OneToManyCascadeDeleteConvention>();
        }

        public DbSet<State> States { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tax> Taxes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<WareHouse> WareHouses { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<PurchaseStatus> PurchaseStatus { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderDetailTmp> OrderDetailTmps { get; set; }
    }
}