using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.Security
{
    public partial class StandardPermissionProvider : IPermissionProvider
    {
        //admin area permissions
        public static readonly PermissionRecord Admin = new PermissionRecord { Name = "Access admin area", SystemName = "Admin", Category = "Area" };
        public static readonly PermissionRecord Permission = new PermissionRecord { Name = "Access Permission", SystemName = "Permission", Category = "Admin" };
        public static readonly PermissionRecord Dashboard = new PermissionRecord { Name = "Access Dashboard", SystemName = "Dashboard", Category = "Admin" };
        public static readonly PermissionRecord User = new PermissionRecord { Name = "Access User", SystemName = "User", Category = "Admin" };
        public static readonly PermissionRecord Product = new PermissionRecord { Name = "Access Product", SystemName = "Product", Category = "Admin" };
        public static readonly PermissionRecord Order = new PermissionRecord { Name = "Access Order", SystemName = "Order", Category = "Admin" };
        public static readonly PermissionRecord RechangeCard = new PermissionRecord { Name = "Access Rechange Card", SystemName = "RechangeCard", Category = "Admin" };
        public static readonly PermissionRecord Customer = new PermissionRecord { Name = "Access Customer", SystemName = "Customer", Category = "Admin" };
        public static readonly PermissionRecord Topic = new PermissionRecord { Name = "Access Topic", SystemName = "Topic", Category = "Admin" };
        public static readonly PermissionRecord Function = new PermissionRecord { Name = "Access Function", SystemName = "Function", Category = "Admin" };
        public static readonly PermissionRecord Files = new PermissionRecord { Name = "Access Files", SystemName = "Files", Category = "Admin" };
        
        /// <summary>
        /// Get permissions
        /// </summary>
        /// <returns>Permissions</returns>
        public virtual IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[]
            {
               Admin,
               Dashboard,
               User,
               Customer,
               Topic,
               Product,
               Order,
               RechangeCard,
               Function,
               Files
            };
        }


    }
}
