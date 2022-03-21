namespace SupplyChainAPI.Domain
{
    using System.Collections.Generic;

    public class ProcessedOrdersWithProducts
    {
        public int Id { get; set; }

        public int BrandId { get; set; }

        public string CustomerName { get; set; }

        public string Reference { get; set; }

        public string OrderDate { get; set; }

        public float PriceTotal { get; set; }

        public List<Product> Products { get; set; }
    }
}
