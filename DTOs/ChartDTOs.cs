namespace Pet_Store_Api.DTOs
{   
    // Can be a Store stock or an overal stock
    public class Chart_SpeciesAmountForStores_DTO
    {
        public required string StoreName { get; set; }
        public required IDictionary<String,int> SpeciesAmount { get; set; }

    }
}
