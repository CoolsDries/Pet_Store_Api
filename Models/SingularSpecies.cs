using System.ComponentModel.DataAnnotations;

namespace Pet_Store_Api.Models
{
    public class SingularSpecies
    {
        [Key]
        public required int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        public required int BasePrice { get; set; }

        public string? Description { get; set; }

        public int InStock { get; set; }

        public ICollection<Animal>? Animals { get; set; }
    }
}
