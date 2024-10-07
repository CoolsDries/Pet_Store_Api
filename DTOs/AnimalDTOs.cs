using Pet_Store_Api.Models;

namespace Pet_Store_Api.DTOs
{
    public class AnimalGetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Discription { get; set; }

        public AnimalGetDTO(Animal animal)
        {
            Id = animal.Id;
            Name = animal.Name;
            Price = animal.Price;
            Discription = animal.Discription;
        }
    }

    public class AnimalPostDTO
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Discription { get; set; }
        public int StoreId { get; set; }
        public int SpeciesId { get; set; }
    }

    static class AnimalDTOMapper
    {
        public static Animal AnimalPostDTO_to_Animal(AnimalPostDTO animalPostDTO)
        {
            return new Animal
            {
                SpeciesId = animalPostDTO.SpeciesId,
                Species = null,
                StoreId = animalPostDTO.StoreId,
                Store = null,
                Name = animalPostDTO.Name,
                Price = animalPostDTO.Price,
                Discription = animalPostDTO.Discription
            };
        }
    }
}