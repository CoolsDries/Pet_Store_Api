using Pet_Store_Api.Models;
using System.Collections.ObjectModel;

namespace Pet_Store_Api.Data
{
    public class DataInitializer
    {
        private readonly PetStoreContext _context;

        public DataInitializer(PetStoreContext context)
        {
            _context = context;
        }

        public void InitializeData()
        {
            // Reset database
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                //Seeding database
                Store store_0 = new Store
                {
                    Name = "Pet Store Sint-Niklaas",
                    Location = "Sint-Niklaas",
                    Animals = []
                };
                Store store_1 = new Store
                {
                    Name = "Pet Store Waasmunster",
                    Location = "Waasmunster",
                    Animals = []
                };
                _context.Stores.AddRange(store_0, store_1);

                // List of spicies
                Collection<String> spiciesList = ["Frog", "Dog", "Cat", "Turtel", "Mouse"];
                foreach (var spicies in spiciesList)
                {
                    Species newSpecies = new Species
                    {
                        Name = spicies,
                        BasePrice = 5,
                    };
                    _context.Species.Add(newSpecies);

                    // Animals located in store_0
                    for (int i = 0; i < 10; i++)
                    {
                        Animal animal = new Animal
                        {
                            Species = newSpecies,
                            Name = spicies + "_" + i.ToString(),
                            store = store_0
                        };
                        _context.Animals.Add(animal);
                    }

                    // Animals located in store_1
                    for (int i = 0; i < 5; i++)
                    {
                        Animal animal = new Animal
                        {
                            Species = newSpecies,
                            Name = spicies + "_" + i.ToString(),
                            store = store_1
                        };
                        _context.Animals.Add(animal);
                    }

                }
                _context.SaveChanges();
            }
        }
    }
}
