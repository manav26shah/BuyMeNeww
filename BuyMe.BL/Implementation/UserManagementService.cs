using BuyMe.BL.Interface;
using BuyMe.DL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.BL
{
    public class UserManagementService:IUserManagementService
    {
        private UserManager<BuyMeUser> _userManager;

        public UserManagementService(UserManager<BuyMeUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Login(string emailId, string password)
        {
            var user = await _userManager.FindByEmailAsync(emailId);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, password);
                return result;
            }
            return false;
        }

        public async Task<Tuple<bool,List<string>>> RegisterUser(RegisterUserBL userDetails)
        {
            var errors = new List<string>();
            var userDL = new BuyMeUser
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Email = userDetails.EmailId,
                PhoneNumber = userDetails.PhoneNumber,
                Address = userDetails.Address,
                DOB = userDetails.DateOfBirth,
                UserName = userDetails.EmailId
            };

            var result = await _userManager.CreateAsync(userDL, userDetails.Password);
            if (result.Succeeded)
            {
                return Tuple.Create(true,new List<string>());
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    errors.Add($"{error.Code}-{error.Description}");
                }
                return Tuple.Create(false, errors);
            }
        }
    }
}
