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
            // Random class
            Random random = new Random();

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

                Dictionary<String, Species> speciesDict = new Dictionary<String, Species>();
                // List of spicies
                Collection<String> spiciesList = ["Frog", "Dog", "Cat", "Turtel", "Mouse"];
                foreach (var spicies in spiciesList)
                {
                    speciesDict[spicies] = new Species
                    {
                        Name = spicies,
                        BasePrice = random.Next(50),
                        Description = spicies + " discription."
                    };
                }

                foreach (var species in speciesDict) {

                    // Initialize species animal list, to avoid possibly null reference.
                    species.Value.Animals ??= [];

                    // Animals located in store_0
                    for (int i = 0; i < random.Next(20); i++)
                    {
                        Animal animal = new Animal
                        {
                            Species = species.Value,
                            Name = species.Key + "_" + i.ToString(),
                            Store = store_0,
                            // Increase baseprice in 1 in 10 cases with a value between 10 and 50
                            Price = species.Value.BasePrice + ((random.Next(9) == 0) ? random.Next(10, 50) : 0),
                            Discription = species.Key + " discription"
                        };

                        species.Value.Animals.Add(animal);
                        store_0.Animals.Add(animal);
                    }

                    // Animals located in store_1
                    for (int i = 0; i < random.Next(10); i++)
                    {
                        Animal animal = new Animal
                        {
                            Species = species.Value,
                            Name = species.Key + "_" + i.ToString(),
                            Store = store_0
                            // Increase baseprice in 1 in 10 cases with a value between 10 and 50
                            Price = species.Value.BasePrice + ((random.Next(9) == 0) ? random.Next(10, 50) : 0),
                            Discription = species.Key + " discription"
                        };

                        species.Value.Animals.Add(animal);
                        store_1.Animals.Add(animal);
                    }

                }
                _context.Stores.AddRange(store_0, store_1);
                _context.SaveChanges();
            }
        }
    }
}
