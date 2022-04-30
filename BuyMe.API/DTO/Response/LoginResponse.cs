using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.DTO.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
    }
}
