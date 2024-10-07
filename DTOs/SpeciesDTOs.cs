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

    static class SpeciesDTOMapper
    {
        public static Species SpeciesPostDTO_to_Species(SpeciesPostDTO speciesPostDTO)
        {
            return new Species
            {
                Name = speciesPostDTO.Name,
                BasePrice = speciesPostDTO.BasePrice,
                Description = speciesPostDTO.Description
            };
        }
    }
}
