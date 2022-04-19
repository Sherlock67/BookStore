
using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAcess
{
    public class ApplicationDbContext :DbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
            {

            }
        public DbSet<Category> categories { get; set; }

        public DbSet<CoverType> CoverTypes { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}
