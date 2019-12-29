using System;

namespace AbakTools.Configuration
{
    class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(string key)
            : base($"Missing or invalid app setting: {key}")
        {
        }
    }
}
