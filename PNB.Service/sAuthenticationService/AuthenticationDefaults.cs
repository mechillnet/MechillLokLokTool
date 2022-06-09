using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sAuthenticationService
{
    public static partial class AuthenticationDefaults
    {
        /// <summary>
        /// The default value used for authentication scheme
        /// </summary>
        public static string AuthenticationScheme => "Authentication";

        public static string AuthenticationBearer => "Bearer";

        /// <summary>
        /// The default value used for external authentication scheme
        /// </summary>
        public static string ExternalAuthenticationScheme => "ExternalAuthentication";

        /// <summary>
        /// The issuer that should be used for any claims that are created
        /// </summary>
        public static string ClaimsIssuer => "PTB";

        /// <summary>
        /// The default value for the login path
        /// </summary>
        public static PathString LoginPath => new PathString("/login");

        /// <summary>
        /// The default value used for the logout path
        /// </summary>
        public static PathString LogoutPath => new PathString("/logout");

        /// <summary>
        /// The default value for the access denied path
        /// </summary>
        public static PathString AccessDeniedPath => new PathString("/page-not-found");

        /// <summary>
        /// The default value of the return URL parameter
        /// </summary>
        public static string ReturnUrlParameter => string.Empty;

        /// <summary>
        /// Gets a key to store external authentication errors to session
        /// </summary>
        public static string ExternalAuthenticationErrorsSessionKey => "nop.externalauth.errors";
    }
}
