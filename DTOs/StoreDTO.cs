using Pet_Store_Api.Models;

namespace Pet_Store_Api.DTOs
{
    public class StoreDTO
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Location { get; set; }

        public StoreDTO(Store store)
        {
            Id = store.Id;
            Name = store.Name;
            Location = store.Location;
        }
    }

}
