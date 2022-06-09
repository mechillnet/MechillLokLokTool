using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNB.Service.AdsServices
{
   
    public partial class AdsService : IAdsService
    {
        private readonly IRepository<Ads> _adsRepository;
        public AdsService(IRepository<Ads> adsRepository)
        {
            _adsRepository = adsRepository;
        }
        public Ads GetAds()
        {
            return _adsRepository.Table.FirstOrDefault();
        }
        public void Update(Ads entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _adsRepository.Update(entity);
        }
      
    }
}
