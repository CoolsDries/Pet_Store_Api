namespace Pet_Store_Api.DTOs
{   
    public class Chart_SpeciesAmountForStores_DTO
    {
        public required string Name { get; set; }
        public required IDictionary<String, int> SpeciesAmounts { get; set; }

    }

    public class Chart_CompareSpeciesAmountForStores_DTO
    {
        public required string Name { get; set; }
        public required Chart_SpeciesAmountForStores_DTO[] Stores { get; set; }
    }
}
