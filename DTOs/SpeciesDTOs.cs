using Pet_Store_Api.Models;

namespace Pet_Store_Api.DTOs
{
    public class SpeciesGetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int BasePrice { get; set; }
        public string? Description { get; set; }

        public SpeciesGetDTO(Species species)
        {
            Id = species.Id;
            Name = species.Name;
            BasePrice = species.BasePrice;
            Description = species.Description;
        }
    }

    public class SpeciesPostDTO
    {
        public string? Name { get; set; }
        public int BasePrice { get; set; }
        public string? Description { get; set; }
    }
}
