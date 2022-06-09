using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using PNB.Service.sAuthenticationService;
using PNB.Service.sUserService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace PNB.Service.Security
{
    public partial class PermissionService : IPermissionService
    {
        private readonly IUserService _userService;
        private readonly IRepository<PermissionRecord> _permissionRecordRepository;
        private readonly IRepository<PermissionCategory> _permissionCategoryRepository;
        private readonly IRepository<PermissionRecordMapping> _permissionRecordRoleMappingRepository;
        private readonly IWorkContext _workContext;
        public PermissionService(
           IUserService userService,
           IRepository<PermissionRecord> permissionRecordRepository,
           IRepository<PermissionCategory> permissionCategoryRepository,
          IRepository<PermissionRecordMapping> permissionRecordRoleMappingRepository,
           IWorkContext workContext)
        {
       
            _userService = userService;
         
            _permissionRecordRepository = permissionRecordRepository;
            _permissionRecordRoleMappingRepository = permissionRecordRoleMappingRepository;
            _permissionCategoryRepository = permissionCategoryRepository;
               _workContext = workContext;
        }
        public virtual void DeletePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));
            _permissionRecordRepository.Delete(permission.Id);
        }

        public virtual PermissionRecord GetPermissionRecordById(int permissionId)
        {
            if (permissionId == 0)
                return null;

            return _permissionRecordRepository.GetById(permissionId);
        }

        public virtual PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            if (string.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from pr in _permissionRecordRepository.Table
                        where pr.SystemName == systemName
                        orderby pr.Id
                        select pr;

            var permissionRecord = query.FirstOrDefault();
            return permissionRecord;
        }
        public virtual IList<PermissionRecord> GetAllPermissionRecords()
        {
            var query = from pr in _permissionRecordRepository.Table
                        orderby pr.Name
                        select pr;
            var permissions = query.ToList();
            return permissions;
        }
        public virtual IList<PermissionCategory> GetAllPermissionCategories()
        {
            var query = from pr in _permissionCategoryRepository.Table
                        orderby pr.Order
                        select pr;
            var categories = query.ToList();
            return categories;
        }

        public virtual void InsertPermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Insert(permission);

           
        }
   
        public virtual void UpdatePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));

            _permissionRecordRepository.Update(permission);
        }

        public virtual bool Authorize(PermissionRecord permission)
        {
            if (string.IsNullOrEmpty(_workContext.CurrentUser.Username))
            {
                return false;
            }
            return Authorize(permission, _workContext.CurrentUser);
        }

        public virtual bool Authorize(PermissionRecord permission, User user)
        {
            if (permission == null)
                return false;

            if (user == null)
                return false;

            //old implementation of Authorize method
            //var customerRoles = customer.CustomerRoles.Where(cr => cr.Active);
            //foreach (var role in customerRoles)
            //    foreach (var permission1 in role.PermissionRecords)
            //        if (permission1.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase))
            //            return true;
            //return false;

            return Authorize(permission.SystemName, user);
        }
        public virtual bool Authorize(string permissionRecordSystemName)
        {
            return Authorize(permissionRecordSystemName, _workContext.CurrentUser);
        }

        public virtual bool Authorize(string permissionRecordSystemName, User user)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;

            var customerRoles = _userService.GetUserRoles(user);
            foreach (var role in customerRoles)
                if (Authorize(permissionRecordSystemName, role.Id))
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }
        public virtual bool Authorize(string permissionRecordSystemName, int customerRoleId)
        {
            if (string.IsNullOrEmpty(permissionRecordSystemName))
                return false;

          
                var permissions = GetPermissionRecordsByCustomerRoleId(customerRoleId).Result;
                foreach (var permission in permissions)
                    if (permission.SystemName.Equals(permissionRecordSystemName, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
          
        }

        public virtual IList<PermissionRecordMapping> GetMappingByPermissionRecordId(int permissionId)
        {
            var query = _permissionRecordRoleMappingRepository.Table;

            query = query.Where(x => x.PermissionRecordId == permissionId);

            return query.ToList();
        }
        public virtual void DeletePermissionRecordCustomerRoleMapping(int permissionId, int customerRoleId)
        {
            var mapping = _permissionRecordRoleMappingRepository.Table.FirstOrDefault(prcm => prcm.CustomerRoleId == customerRoleId && prcm.PermissionRecordId == permissionId);

            if (mapping is null)
                throw new Exception(string.Empty);

            _permissionRecordRoleMappingRepository.Delete(mapping);

      
        }

        public virtual void InsertPermissionRecordUserRoleMapping(PermissionRecordMapping permissionRecordCustomerRoleMapping)
        {
            if (permissionRecordCustomerRoleMapping is null)
                throw new ArgumentNullException(nameof(permissionRecordCustomerRoleMapping));

            _permissionRecordRoleMappingRepository.Insert(permissionRecordCustomerRoleMapping);

        }



        // Extension
        protected virtual async Task<IList<PermissionRecord>> GetPermissionRecordsByCustomerRoleId(int customerRoleId)
        {
          
            var query = from pr in _permissionRecordRepository.Table
                        join prcrm in _permissionRecordRoleMappingRepository.Table on pr.Id equals prcrm
                            .PermissionRecordId
                        where prcrm.CustomerRoleId == customerRoleId
                        orderby pr.Id
                        select pr;

            return await query.ToListAsync();
        }

    }
}
