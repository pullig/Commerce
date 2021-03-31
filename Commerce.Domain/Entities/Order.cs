using System;

namespace Commerce.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }

        //public virtual List<ProductOrder> ProductOrders { get; set; }
    }
}
