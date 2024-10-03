using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Store_Api.Models
{
    // Question: Store > Species > Animals
    // or Store > Animals & Species > Animals
    // Which one is more efficient?
    public class Store
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Location { get; set; }

        public ICollection<Animal>? Animals { get; set; }
    }
}
