using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuyMe.BL.Interface
{
    public interface IUserManagementService
    {
        public Task<Tuple<bool, List<string>>> RegisterUser(RegisterUserBL userDetails);
        public Task<bool> Login(string emailId, string password);
    }
}
