using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.FacebookApi
{
    public interface IFacebookApiService
    {
        public string GetSourceVideoById(string videoId);
        public bool CheckLink(string Link);
    }
}
