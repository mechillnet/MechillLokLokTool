﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Web.Framework.Extensions
{
    public partial class HashingPassword
    {

        private static string GetRandomSalt()
        {

            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
