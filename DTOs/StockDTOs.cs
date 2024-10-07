namespace Pet_Store_Api.DTOs
{   
    // Can be a Store stock or an overal stock
    public class StockDTO
    {
        public required string StoreName { get; set; }
        public required List<SpeciesStockDTO> SpeciesStocks { get; set; }

    }

    public class SpeciesStockDTO
    {
        public required string SpeciesName { get; set; }
        public required int AnimalsAmount { get; set; }
    }
}
