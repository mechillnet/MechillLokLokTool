using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sSendEmail
{
    public interface ISendEmailService
    {
        void SendEmail(string subject, string body, string fromAddress, string fromName, string toAddress, string toName);
    }
}
