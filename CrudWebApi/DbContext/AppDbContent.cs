using CrudWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudWebApi.AppDbContext
{
    public class AppDbContent : DbContext
    {
        public AppDbContent(DbContextOptions<AppDbContent> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
