namespace Pet_Store_Api.DTOs
{   
    // Can be a Store stock or an overal stock
    public class StockDTO
    {
        public required StoreDTO storeDTO { get; set; }
        public required List<SpeciesStockDTO> SpeciesStocks { get; set; }

    }
}
