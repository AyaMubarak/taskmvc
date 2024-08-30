using Microsoft.EntityFrameworkCore;

namespace taskmvc.Models.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

       
        public DbSet<User> Users { get; set; }
    }
}

