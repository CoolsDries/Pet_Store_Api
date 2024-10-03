namespace Pet_Store_Api.DTOs
{   
    // Can be a Store stock or an overal stock
    public class StockDTO
    {
        public required StoreGetDTO storeGetDTO { get; set; }
        public required List<SpeciesStockDTO> SpeciesStocks { get; set; }

    }

    public class SpeciesStockDTO
    {
        public required SpeciesGetDTO speciesGetDTO { get; set; }
        public int AnimalsAmount { get; set; }
    }
}
