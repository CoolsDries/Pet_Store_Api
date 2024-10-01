using Microsoft.EntityFrameworkCore;
using Pet_Store_Api.Models;

namespace Pet_Store_Api.Data
{
    public class PetStoreContext : DbContext
    {
        public PetStoreContext(DbContextOptions<PetStoreContext> options) : base(options)
        {
        }

        // Is not necessary, model automatically adds the ForeignKeys to Animal
        // because animal contains a Store and Species object
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Store>()
            //    .HasMany(s => s.Animals)
            //    .WithOne()
            //    .HasForeignKey("StoreId");

            //modelBuilder.Entity<Species>()
            //    .HasMany(s => s.Animals)
            //    .WithOne()
            //    .HasForeignKey("SpeciesId");
        }*/

        public DbSet<Store> Stores { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Species> Species { get; set; }

    }
}
