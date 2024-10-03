using Pet_Store_Api.Models;

namespace Pet_Store_Api.DTOs
{
    public class StoreGetDTO
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Location { get; set; }

        public StoreGetDTO() { }

        public StoreGetDTO(Store store)
        {
            Id = store.Id;
            Name = store.Name;
            Location = store.Location;
        }
    }

    public class StorePostDTO
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
    }

}
