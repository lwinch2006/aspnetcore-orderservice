namespace OrderService
{
    public class OrderLine
    {
        public OrderLine(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }
        public int Quantity { get; }
    }
}