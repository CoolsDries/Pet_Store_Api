using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Store_Api.Models
{
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required Species Species { get; set; }

        [Required]
        public required Store Store { get; set; }

        public string? Name { get; set; }
        
        public int Prijs { get; set; }

        public string? Beschrijving { get; set; }
    }
}
