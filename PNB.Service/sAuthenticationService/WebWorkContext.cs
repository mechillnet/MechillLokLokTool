using Microsoft.AspNetCore.Http;
using PNB.Domain.Models;
using PNB.Service.sUserService;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sAuthenticationService
{
    public partial class WebWorkContext : IWorkContext
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _cachedUser;
        private User _originalUserIfImpersonated;
        public WebWorkContext(
         IAuthenticationService authenticationService,
        IUserService userService,
         IHttpContextAccessor httpContextAccessor
     )
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
          
        }
        protected virtual string GetUserCookie()
        {
            var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.CustomerCookie}";
            return _httpContextAccessor.HttpContext?.Request?.Cookies[cookieName];
        }

        /// <summary>
        /// Set nop customer cookie
        /// </summary>
        /// <param name="customerGuid">Guid of the customer</param>
        protected virtual void SetUserCookie(Guid userGuid)
        {
            if (_httpContextAccessor.HttpContext?.Response == null)
                return;

            //delete current cookie value
            var cookieName = $"{CookieDefaults.Prefix}{CookieDefaults.CustomerCookie}";
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(cookieName);

            //get date of cookie expiration
            var cookieExpires = 24 * 365; //TODO make configurable
            var cookieExpiresDate = DateTime.Now.AddHours(cookieExpires);

            //if passed guid is empty set cookie as expired
            if (userGuid == Guid.Empty)
                cookieExpiresDate = DateTime.Now.AddMonths(-1);

            //set new cookie value
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = cookieExpiresDate
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, userGuid.ToString(), options);
        }
        public virtual User CurrentUser
        {
            get
            {
                //whether there is a cached value
                if (_cachedUser != null)
                    return _cachedUser;

                User user = null;



                if (user == null || user.Deleted || !user.Active)
                {
                    //try to get registered user
                    user = _authenticationService.GetAuthenticatedUser();
                }

                if (user == null || user.Deleted || !user.Active)
                {
                    var User = new User();
                    User.Guid = new Guid();
                    User.Active = true;

                    User.CreatedOn = DateTime.UtcNow;
                    //create guest if not exists
                    user = User;
                }

                if (!user.Deleted && user.Active)
                {
                    //set customer cookie
                    SetUserCookie(user.Guid);

                    //cache the found customer
                    _cachedUser = user;
                }
            

                return _cachedUser;
            }
            set
            {
                SetUserCookie(value.Guid);
                _cachedUser = value;
            }
        }
        public virtual User OriginalUserIfImpersonated
        {
            get { return _originalUserIfImpersonated; }
        }
        public virtual bool IsAdmin { get; set; }
    }
}
