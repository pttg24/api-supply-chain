namespace SupplyChainAPI.Domain
{
    public class Product
    {
        public Product(string productName, int quantity)
        {
            this.ProductName = productName;
            this.Quantity = quantity;
        }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
