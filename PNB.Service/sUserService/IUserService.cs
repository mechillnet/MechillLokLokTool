using PNB.Domain.Common;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sUserService
{
    public partial interface IUserService
    {
        IPagedList<User> GetAll(DateTime? From,DateTime? To,string email = "", string phone = "", string name = "", string username = "",bool? status = null, int start =0 , int lenght =15);
        User GetById(int Id);
        IEnumerable<User> GetAllUserByRoles(string RoleId);
        IList<Role> GetUserRoles(User user);
        User GetByEmail(string email);

        User GetByUsername(string username);
        void Insert(User user);
        void Update(User user);
        void Delete(User user);
    }
}
