using BuyMe.DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.BL.Interface
{
    public interface IUserManagementService
    {
        public  Task<bool> CheckIfUserExistAndEmailIsNotConfirmed(string email);
        public Task<Tuple<bool, List<string>>> RegisterUser(RegisterUserBL userDetails, bool isAdmin=false);
        public Task<bool> Login(string emailId, string password);
        public Task<string> GenerateVerifyEmailToken(string email);
        public Task<bool> VerifyEmail(string email, string token);
        public Task<BuyMeUser> GetuserDetails(string email);
        public Task<IList<string>> GetRoles(string email);
    }
}
