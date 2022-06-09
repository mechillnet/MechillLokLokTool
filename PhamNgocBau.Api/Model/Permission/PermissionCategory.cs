using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Model.Permission
{
    public class PermissionCategoryModel
    {
        public int Id { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public bool Enable { get; set; }
        public IList<PermissionRecordModel> AvailablePermission{ get; set; }
    }
}
