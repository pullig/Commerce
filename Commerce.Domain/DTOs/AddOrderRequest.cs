using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.DTOs
{
    public class AddOrderRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public List<AddOrderProductRequest> Products { get; set; }
    }
}
