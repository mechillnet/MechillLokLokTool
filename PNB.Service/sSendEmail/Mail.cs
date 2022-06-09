using PNB.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sSendEmail
{
   public partial class Mail:ISettings
    {
        public string UserName { get; set; }
        public bool Ssl { get; set; }
        public string Smtp { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}
