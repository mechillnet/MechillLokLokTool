using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Permission
{
    public class PermisssionModel
    {

        public PermisssionModel()
        {
            AvailablePermissions = new List<PermissionCategoryModel>();
        }
        public IList<PermissionCategoryModel> AvailablePermissions { get; set; }
    
    }
}
