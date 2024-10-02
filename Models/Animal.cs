using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pet_Store_Api.Models
{
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Define foreign key's in class, for easier use repositories, database will assine 
        // the foreign key's automaticly.
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        [ForeignKey("Species")]
        public int SpeciesId { get; set; }

        [Required]
        public required Species Species { get; set; }

        [Required]
        public required Store Store { get; set; }

        public string? Name { get; set; }
        
        // Posibility to overwrite baseprice set by Species, in case of rare animal
        public int Price { get; set; }

        public string? Discription { get; set; }
    }
}
