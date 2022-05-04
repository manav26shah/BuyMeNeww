using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.services
{
    public class MockEmailSender : IEmailSender
    {
        public bool SendEmail(string to, string subject, string body)
        {
            File.AppendAllText("Email.txt", $"Email-{to},verifyToken {body}");
            return true;
        }
    }
}
