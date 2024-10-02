using Pet_Store_Api.Models;

namespace Pet_Store_Api.DTOs
{
    public class SpeciesDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int BasePrice { get; set; }
        public string? Description { get; set; }
        public int InStock { get; set; }

        public SpeciesDTO(Species species)
        {
            Id = species.Id;
            Name = species.Name;
            BasePrice = species.BasePrice;
            Description = species.Description;
            InStock = species.InStock;
        }
    }
}
