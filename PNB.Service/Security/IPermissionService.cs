using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.Security
{
    public  interface IPermissionService
    {
      
        void DeletePermissionRecord(PermissionRecord permission);

    
        PermissionRecord GetPermissionRecordById(int permissionId);

        PermissionRecord GetPermissionRecordBySystemName(string systemName);

       
        IList<PermissionRecord> GetAllPermissionRecords();
        IList<PermissionCategory> GetAllPermissionCategories();


        void InsertPermissionRecord(PermissionRecord permission);

        void UpdatePermissionRecord(PermissionRecord permission);

     
        bool Authorize(PermissionRecord permission);

      
        bool Authorize(PermissionRecord permission, User user);

  
        bool Authorize(string permissionRecordSystemName);

   
        bool Authorize(string permissionRecordSystemName, User user);

    
        bool Authorize(string permissionRecordSystemName, int customerRoleId);

  
        IList<PermissionRecordMapping> GetMappingByPermissionRecordId(int permissionId);

        void DeletePermissionRecordCustomerRoleMapping(int permissionId, int customerRoleId);

    
        void InsertPermissionRecordUserRoleMapping(PermissionRecordMapping permissionRecordCustomerRoleMapping);
    }
}
