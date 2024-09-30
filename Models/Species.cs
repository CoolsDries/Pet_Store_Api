namespace Pet_Store_Api.Models
{
    public class Species
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required int BasePrice { get; set; }
        public string? Description { get; set; }
        public int InStock { get; set; }
        public ICollection<Animal>? Animals { get; set; }
    }
}
