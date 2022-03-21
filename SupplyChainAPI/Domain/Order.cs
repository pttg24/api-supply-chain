namespace SupplyChainAPI.Domain
{
    public class Order
    {
        public Order(int id, int brandId, string customerName, string reference, string orderDate, float priceTotal)
        {
            this.Id = id;
            this.BrandId = brandId;
            this.CustomerName = customerName;
            this.Reference = reference;
            this.OrderDate = orderDate;
            this.PriceTotal = priceTotal;
        }

        public int Id { get; set; }

        public int BrandId { get; set; }

        public string CustomerName { get; set; }

        public string Reference { get; set; }

        public string OrderDate { get; set; }

        public float PriceTotal { get; set; }
    }
}
