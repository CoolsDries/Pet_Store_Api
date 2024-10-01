using Pet_Store_Api.Models;

namespace Pet_Store_Api.DTOs
{
    public class StoreDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        //TODO: change to AnimalDTO
        public ICollection<Animal>? Animals { get; set; }

        public StoreDTO(Store store)
        {
            Name = store.Name;
            Location = store.Location;
            Animals = store.Animals;
        }
    }

}
