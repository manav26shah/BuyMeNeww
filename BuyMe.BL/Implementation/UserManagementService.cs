using BuyMe.BL.Constants;
using BuyMe.BL.Interface;
using BuyMe.DL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.BL
{
    public class UserManagementService : IUserManagementService
    {
        private UserManager<BuyMeUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserManagementService(UserManager<BuyMeUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> Login(string emailId, string password)
        {
            var user = await _userManager.FindByEmailAsync(emailId);
            if (user != null)
            {
                if (user.EmailConfirmed == false)
                {
                    return false;
                }
                var result = await _userManager.CheckPasswordAsync(user, password);
                return result;
            }
            return false;
        }

        public async Task<bool> CheckIfUserExistAndEmailIsNotConfirmed(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            if (user.EmailConfirmed == false)
            {
                return true;
            }
            return false;
        }
        public async Task<Tuple<bool, List<string>>> RegisterUser(RegisterUserBL userDetails, bool isAdmin=false)
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
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    
                }
                if (isAdmin)
                {
                    await _userManager.AddToRoleAsync(userDL, UserRoles.Admin);
                }
                return Tuple.Create(true, new List<string>());
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

        public async Task<string> GenerateVerifyEmailToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<bool> VerifyEmail(string email, string token)
        {


            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<BuyMeUser> GetuserDetails(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles;
        }
    }
}
