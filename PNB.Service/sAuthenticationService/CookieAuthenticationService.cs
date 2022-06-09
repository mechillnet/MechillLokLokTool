
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using PNB.Domain.Models;
using PNB.Service.sUserService;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PNB.Service.sAuthenticationService
{
    public partial class CookieAuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _cachedUser;
        private const string TypeClaimPassword = "Password";
        public CookieAuthenticationService(
           IUserService userService,
           IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }
        public virtual async void SignIn(User user, bool isPersistent)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //create claims for customer's username and email
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.Username))
            {
                claims.Add(new Claim(ClaimTypes.Name, user.Username, ClaimValueTypes.String, AuthenticationDefaults.ClaimsIssuer));
                claims.Add(new Claim(TypeClaimPassword, user.Password, ClaimValueTypes.String, AuthenticationDefaults.ClaimsIssuer));
            }
              

           

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, AuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.Now
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(AuthenticationDefaults.AuthenticationScheme, userPrincipal, authenticationProperties);

            //cache authenticated customer
            _cachedUser = user;
        }
        public virtual async void SignOut()
        {
            //reset cached customer
            _cachedUser = null;

            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(AuthenticationDefaults.AuthenticationScheme);
        }
        public virtual User GetAuthenticatedUser()
        {
            //whether there is a cached customer
            if (_cachedUser != null)
                return _cachedUser;

            //try to get authenticated user identity
            var authenticateResult = _httpContextAccessor.HttpContext.AuthenticateAsync(AuthenticationDefaults.AuthenticationBearer).Result;
            if (!authenticateResult.Succeeded)
                return null;

            User user = null;
          
                //try to get customer by username
                var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name);
                var PasswordClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == TypeClaimPassword);
            if (usernameClaim != null)
                user = _userService.GetByUsername(usernameClaim.Value);
        

            //whether the found customer is available
            if (user == null || !user.Active || user.Deleted)
                return null;

            //cache authenticated customer
            _cachedUser = user;

            return _cachedUser;
        }
    }
}
