using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace PNB.Service.sStore
{
    public partial class StoreService : IStoreService
    {
        private IRepository<Store> _storeRepository;

        public StoreService(IRepository<Store> storeRepository)
        {
            _storeRepository = storeRepository;
        }
        public Store Get()
        {
            return _storeRepository.Table.FirstOrDefault();
        }
        public void Update(Store entity)
        {
            _storeRepository.Update(entity);
        }
    }
}
