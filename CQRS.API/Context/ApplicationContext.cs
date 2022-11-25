using CQRS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Country> Countries { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();
    }
}
