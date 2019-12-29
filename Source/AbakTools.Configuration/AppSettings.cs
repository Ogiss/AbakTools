using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Globalization;


namespace AbakTools.Configuration
{
    public static class AppSettings
    {
        public static string EnovaPath { get; private set; }
        public static string EnovaDatabase { get; private set; }

        internal static void Load()
        {
            EnovaPath = LoadString("EnovaPath", true);
            EnovaDatabase = LoadString("EnovaDatabase", true);
        }

        private static string LoadString(string key, bool required)
        {
            string setting = ConfigurationManager.AppSettings[key];

            if (required && String.IsNullOrEmpty(setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

        private static int LoadInt(string key, bool required)
        {
            string settingStr = LoadString(key, required);

            if (!Int32.TryParse(settingStr, out int setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

        private static long LoadLong(string key, bool required)
        {
            string settingStr = LoadString(key, required);

            if (!Int64.TryParse(settingStr, out long setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

        private static decimal LoadDecimal(string key, bool required)
        {
            string settingStr = LoadString(key, required);

            if (!Decimal.TryParse(settingStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

        private static Guid LoadGuid(string key, bool required)
        {
            string settingsStr = LoadString(key, required);

            if (!Guid.TryParse(settingsStr, out Guid settings))
            {
                throw new InvalidConfigurationException(key);
            }

            return settings;
        }

        private static T LoadEnum<T>(string key, bool required)
            where T : struct
        {
            string settingStr = LoadString(key, required);

            if (!Enum.TryParse(settingStr, out T setting))
            {
                throw new InvalidConfigurationException(key);
            }

            return setting;
        }

    }
}
