namespace Pet_Store_Api.DTOs
{
    public class SpeciesStockDTO
    {
        public required SpeciesDTO speciesDTO {  get; set; }
        public int AnimalsAmount { get; set; }
    }
}
