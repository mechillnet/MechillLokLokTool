using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sStore
{
   public interface IStoreService
    {
        Store Get();
        void Update(Store entity);
    }
}
