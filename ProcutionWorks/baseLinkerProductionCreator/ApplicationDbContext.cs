using Microsoft.EntityFrameworkCore;

namespace BaseLinkerProductionCreator
{
    class ApplicationDbContext : DbContext
    {
        public DbSet<Image> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Image>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string constr = Utility.GetConnectionString("ConnectionStrings:DefaultConnection");
            optionsBuilder.UseSqlServer(constr);
        }

    }
}
