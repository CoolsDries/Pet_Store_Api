using Pet_Store_Api.Models;

namespace Pet_Store_Api.DTOs
{
    public class StoreDTO
    {
        public string Name { get; set; }
        public string Location { get; set; }
        //TODO: change to SpeciesDTO
        public ICollection<Species>? SpeciesList { get; set; }

        public StoreDTO(Store store)
        {
            Name = store.Name;
            Location = store.Location;
            SpeciesList = store.SpeciesList;
        }
    }

}
