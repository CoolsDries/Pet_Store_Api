namespace Pet_Store_Api.Models
{
    public class Animal
    {
        public required int Id { get; set; }
        public string? Name { get; set; }
        public required Species Species { get; set; }
        public required int Prijs { get; set; }
        public string? Beschrijving { get; set; }
    }
}
