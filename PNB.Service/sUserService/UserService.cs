using PNB.Domain;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PNB.Service.sUserService
{
     public partial class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private IRepository<Role> _roleRepository;
        public UserService(IRepository<User> userRepository, IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public IList<Role> GetUserRoles(User user)
        {
            var ListRole = user.RoleId.Split(",");
            List<Role> roles = new List<Role>();
             foreach(var RoleId in ListRole)
            {
                var role = from r in _roleRepository.Table where r.Id == int.Parse(RoleId) select r;
                roles.Add(role.FirstOrDefault());
            }
            return roles;
        }

        public IPagedList<User> GetAll(DateTime? From, DateTime? To, string email = "", string phone = "", string name = "", string username = "", bool? status = null, int start =0 , int lenght = 15)
        {
            var query =_userRepository.Table;
            if(From != null)
            {
                query = query.Where(x => x.CreatedOn >= From);
            }
            if (To != null)
            {
                query = query.Where(x => x.CreatedOn <= To);
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(x => x.Email.Contains(email));
            }
            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(x => x.Phone.Contains(phone));
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(x => x.Username.Contains(username));
            }
            if (status!=null)
            {
                query = query.Where(x => x.Active==status);
            }
            return new PagedList<User>(query, start, lenght);
        }
        public IEnumerable<User> GetAllUserByRoles(string RoleId)
        {
            var query =  from u in  _userRepository.Table where u.RoleId == RoleId && u.Active == true select u;
            return query;
        }
        public  User GetById(int Id)
        {
            if (Id == 0)
                return null;
            return _userRepository.GetById(Id);
        }

      public  User GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            var query = from u in _userRepository.Table
                        where u.Email == email
                        select u;
            return query.FirstOrDefault();
        }

       public User GetByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return null;
            var query = from u in _userRepository.Table
                        where u.Username == username
                        select u;
            return query.FirstOrDefault();
        }
        public void Insert(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _userRepository.Insert(user);

        }
        public void Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _userRepository.Update(user);
        }
        public void Delete(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _userRepository.Delete(user.Id);
        }
    }
}
