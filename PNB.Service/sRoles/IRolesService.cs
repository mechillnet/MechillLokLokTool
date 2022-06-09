using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sRoles
{
    
        public interface IRolesService
        {
            IPagedList<Role> GetAll(bool getall ,int start = 0, int take = 15);

            Role GetById(int Id);
            void Insert(Role file);
            void Update(Role file);
            void Delete(Role file);

    }
}
