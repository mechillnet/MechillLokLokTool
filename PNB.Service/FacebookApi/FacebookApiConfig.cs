using PNB.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.FacebookApi
{
   public class FacebookApiConfig :ISettings
    {
        public string token { get; set; }
        public string host { get; set; }
    }
}
