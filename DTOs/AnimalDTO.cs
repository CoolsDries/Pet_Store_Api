using Pet_Store_Api.Models;

namespace Pet_Store_Api.DTOs
{
    public class AnimalDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Prijs { get; set; }
        public string? Beschrvijng { get; set; }

        public AnimalDTO(Animal animal)
        {
            Id = animal.Id;
            Name = animal.Name;
            Prijs = animal.Prijs;
            Beschrvijng = animal.Beschrijving;
        }
    }
}