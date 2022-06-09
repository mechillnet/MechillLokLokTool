using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PNB.Domain;

namespace PNB.Service.sRoles
{
    public partial   class RolesService : IRolesService
    {
        private readonly IRepository<Role> _rolerepository;
        public RolesService(IRepository<Role> rolerepository)
        {
            _rolerepository = rolerepository;
        }
        public IPagedList<Role> GetAll(bool getall ,int start = 0, int take = 15)
        {
            var query = from h in _rolerepository.Table select h;
            if (getall)
                return new PagedList<Role>(query);
           
            return new PagedList<Role>(query, start, take);
        }

        public Role GetById(int Id)
        {
            if (Id == 0)
                return null;
            return _rolerepository.GetById(Id);
        }
        public void Insert(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _rolerepository.Insert(role);
        }
        public void Update(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _rolerepository.Update(role);
        }
        public void Delete(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _rolerepository.Delete(role.Id);
        }
    }
}
