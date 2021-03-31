using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Domain.DTOs
{
    public class SignInResult
    {
        public SignedUser User { get; set; }
        public string Token { get; set; }
    }
}
