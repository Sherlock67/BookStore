
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

    }
}
