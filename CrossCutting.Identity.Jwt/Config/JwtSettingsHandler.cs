using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace CrossCutting.Identity.Jwt.Config
{
    public static class JwtSettingsHandler
    {
        private static JwtSettingsContainer _jwtSettings;
        public static JwtSettingsContainer Settings 
        { 
            get
            {
                if (_jwtSettings != null) return _jwtSettings;

                var configurationBuilder = new ConfigurationBuilder();
                var jwtsettingsPath = GetJwtSettingsFilePath();

                configurationBuilder.AddJsonFile(jwtsettingsPath, false);

                var root = configurationBuilder.Build();
                _jwtSettings = root.GetSection("JwtSettings").Get<JwtSettingsContainer>();

                return _jwtSettings;
            }
        }

        public static void ReloadSettings()
        {
            _jwtSettings = null;
        }

        private static String GetJwtSettingsFilePath()
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var jwtsettingsPath = Path.Combine(location, "jwtsettings.json");
            return jwtsettingsPath;
        }
    }
}