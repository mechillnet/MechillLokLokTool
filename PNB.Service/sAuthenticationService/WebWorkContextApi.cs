using Microsoft.AspNetCore.Http;
using PNB.Domain.Models;
using PNB.Service.sUserService;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sAuthenticationService
{
    public partial class WebWorkContextApi : IWorkContext
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _cachedUser;
        private User _originalUserIfImpersonated;
        public WebWorkContextApi(
         IAuthenticationService authenticationService,
        IUserService userService,
         IHttpContextAccessor httpContextAccessor
     )
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;

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
                    //cache the found customer
                    _cachedUser = user;
                }


                return _cachedUser;
            }
            set
            {
          
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
