using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.DTOs
{
    public class GetOrderResult
    {
        public int Id { get; set; }
        public GetUserResult User { get; set; }
        public List<GetProductOrdersResult> Products { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal TotalPrice 
        { 
            get
            {
                var total = 0M;
                foreach (var product in Products)
                {
                    total += product.UnityPrice * product.Quantity;
                }
                return total;
            }
        }

    }
}
