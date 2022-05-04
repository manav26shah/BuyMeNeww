using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.services
{
    public interface IEmailSender
    {
        public bool SendEmail(string to, string subject, string body);
    }
}
