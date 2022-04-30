using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.DL.Entities
{
    public class BuyMeUser:IdentityUser
    {
        public string Address { get; set; }
        public DateTime? DOB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


   
}
