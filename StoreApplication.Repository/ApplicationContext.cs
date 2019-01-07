using Microsoft.EntityFrameworkCore;

namespace StoreApplication.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions context) : base(context)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }
    }
}
