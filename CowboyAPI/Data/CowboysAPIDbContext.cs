using CowboyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CowboyAPI.Data
{
    public class CowboysAPIDbContext:DbContext
    {
        public CowboysAPIDbContext(DbContextOptions options) : base(options)
        {
                
        }

        public DbSet<Cowboy> Cowboys { get; set; }
    }
}
