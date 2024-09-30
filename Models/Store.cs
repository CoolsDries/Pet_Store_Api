using System.ComponentModel.DataAnnotations;

namespace Pet_Store_Api.Models
{
    public class Store
    {
        [Key]
        public required int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Location { get; set; }

        public ICollection<SingularSpecies>? Species { get; set; }
    }
}
