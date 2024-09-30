namespace Pet_Store_Api.Models
{
    public class Store
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public ICollection<Species>? SpeciesList { get; set; }
    }
}
