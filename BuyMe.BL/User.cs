using BuyMe.BL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace BuyMe.BL
{



    // written 10 years ago
    public class User : IUserService
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string Pincode { get; set; }

        public User()
        {
            Debug.WriteLine("Obj created");
        }

        public User GetuserDetails(int userId)
        {
            return new User { Adress = "Test", UserId = "Abc", UserName = "DEF" };
        }
    }

   
}
