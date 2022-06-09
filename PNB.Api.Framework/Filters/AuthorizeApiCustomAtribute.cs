using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PNB.Api.Framework.Common;
using PNB.Service.sUserService;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Api.Framework.Mvc.Filters
{

    public class AuthorizeApiCustomAttribute : TypeFilterAttribute
    {
        private readonly string mPermission; //Permission string to get from controller
        public AuthorizeApiCustomAttribute(string Permission = "")
        : base(typeof(AuthorizeActionFilter))
        {
            mPermission = Permission;
            Arguments = new object[] { Permission };
        }
        public string Permission => mPermission;
    }
    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly string mPermission;
        private readonly IUserService _userService;
        public AuthorizeActionFilter(string permission, IUserService userService)
        {
            mPermission = permission;
            _userService = userService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new CustomUnauthorizedResult("Xác thực đăng nhập thất bại");
                return;
            }
            var UserName = context.HttpContext.User.Identity.Name;
            var User = _userService.GetByUsername(UserName);
            if (User == null)
            {
                context.Result = new CustomUnauthorizedResult("Tài khoản không tồn tại");
                return;
            }
            if (!User.Active)
            {
                context.Result = new CustomUnauthorizedResult("Tài khoản đang bị khóa");
                return;
            }
        }
    }
    public class CustomUnauthorizedResult : JsonResult
    {
        public CustomUnauthorizedResult(string messages)
            : base(new CustomError(messages))
        {
            StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
    public class CustomError
    {
        public string message { get; }
        public int status { get; set; }
        public object data { get; set; }
        public CustomError(string messages)
        {
            message = messages;
            status = (int)BaseApiStatus.Authorization401;
            data = new object();
        }
    }
}
