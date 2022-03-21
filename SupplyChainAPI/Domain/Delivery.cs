namespace SupplyChainAPI.Domain
{
    using System.Collections.Generic;

    public class Delivery
    {
        public Delivery(int id, int orderId, bool shipped, List<Product> products)
        {
            this.Id = id;
            this.OrderId = orderId;
            this.Shipped = shipped;
            this.Products = products;
        }

        public int Id { get; set; }

        public int OrderId { get; set; }

        public bool Shipped { get; set; }

        public List<Product> Products { get; set; }

    }
}
