using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Constants
{
    public class AuthorizationConstants
    {
        public const string AUTH_KEY = "AuthKeyOfDoomThatMustBeAMinimumNumberOfBytes";

        // TODO: Don't use this in production
        public const string DEFAULT_PASSWORD = "Pass@word1";

        // TODO: Change this to an environment variable
        public const string JWT_SECRET_KEY = "ThisIsASecretKeyForJWTGeneration1234!";
    }
}
