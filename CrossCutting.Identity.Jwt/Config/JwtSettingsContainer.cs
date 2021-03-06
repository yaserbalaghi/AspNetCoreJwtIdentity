﻿using System;

namespace CrossCutting.Identity.Jwt.Config
{
    public class JwtSettingsContainer
    {
        public String Audience { get; set; }
        public String Issuer { get; set; }
        public String SecretKey { get; set; }
        public String EncryptKey { get; set; }
        public Double NotBeforeMinutes { get; set; }
        public Double ExpirationDay { get; set; }
        public String ConnectionString { get; set; }
        public Boolean PasswordRequireDigit { get; set; }
        public Int32 PasswordRequiredLength { get; set; }
        public Boolean PasswordRequireNonAlphanumeric { get; set; } 
        public Boolean PasswordRequireUppercase { get; set; }
        public Boolean PasswordRequireLowercase { get; set; }
        public Boolean RequireUniqueEmail { get; set; }
    }
}
