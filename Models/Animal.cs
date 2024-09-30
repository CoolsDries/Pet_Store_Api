using System.ComponentModel.DataAnnotations;

namespace Pet_Store_Api.Models
{
    public class Animal
    {
        [Key]
        public required int Id { get; set; }

        [Required]
        public required SingularSpecies SingularSpecies { get; set; }

        public string? Name { get; set; }
        
        public int Prijs { get; set; }

        public string? Beschrijving { get; set; }
    }
}
