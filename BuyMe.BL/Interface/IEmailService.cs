using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BuyMe.BL.Models;

namespace BuyMe.BL.Interface
{
    public interface IEmailService
    {
        Task SendEmailForForgotPassword(UserEmailOptions options);
    }
} 
