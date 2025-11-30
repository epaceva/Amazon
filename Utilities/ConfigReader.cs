#nullable disable
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Amazon.Utilities
{
    public static class ConfigReader
    {
        private static IConfigurationRoot _configuration;

        static ConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public static string GetUrl()
        {
            return _configuration["AppConfig:BaseUrl"];
        }
    }
}