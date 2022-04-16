using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMe.BL.Interface
{
    public interface IUserService
    {
        User GetuserDetails(int userId);
    }
}
