namespace Commerce.Domain.Entitties
{
    public class ProductOrder
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UnityPrice { get; set; }

        public virtual Product Product { get; set; }
    }
}
