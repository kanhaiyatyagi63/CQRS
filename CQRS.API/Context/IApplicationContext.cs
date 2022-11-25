using CQRS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRS.API.Context
{
    public interface IApplicationContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Project> Projects { get; set; }

        Task<int> SaveChangesAsync();
    }
}