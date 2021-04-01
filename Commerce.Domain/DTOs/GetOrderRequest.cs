using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.DTOs
{
    public class GetOrderRequest
    {
        public string Username { get; set; }
        public string ProductName { get; set; }
        public int? OrderId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
