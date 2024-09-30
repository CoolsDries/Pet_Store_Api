using Microsoft.EntityFrameworkCore;
using Pet_Store_Api.Models;

namespace Pet_Store_Api.Data
{
    public class PetStoreContext : DbContext
    {
        public PetStoreContext(DbContextOptions<PetStoreContext> options) : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; } = null!;

    }
}
