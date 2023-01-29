using LogAndPass.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace LogAndPass.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        

    }
}
