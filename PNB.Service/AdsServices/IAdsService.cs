using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.AdsServices
{
    public interface IAdsService
    {
      
        Ads GetAds();
        void Update(Ads entity);

    }
   
}
