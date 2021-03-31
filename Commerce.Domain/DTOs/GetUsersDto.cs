using Commerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.DTOs
{
    public class GetUsersDto
    {
        public string Username { get;set; }

        public string EmailAddress { get; set; }

        public string DisplayName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public UserOrderBy OrderBy { get; set; }
    }
}
