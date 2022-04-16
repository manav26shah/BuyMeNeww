using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.DL.Entities
{
    public class AppUser:IdentityUser
    {
        public string City { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
    }
}
