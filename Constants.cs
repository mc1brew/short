using Microsoft.Extensions.Configuration;

namespace Short
{
    public static class Constants
    {
        public readonly static string Favicon = "favicon.ico";
        public static class ConfigurationKeys
        {
            public readonly static string AddUrl = "Shorter:AddUrl";
            public readonly static string ApiUrl = "Shorter:ApiUrl";
            public readonly static string HostKey = "Shorter:HostKey";
            public readonly static string RedirectUrl = "Shorter:RedirectUrl";
        }

        public static class Configuration
        {
            public static string AddUrl => Startup.StaticConfig.GetValue<string>(Constants.ConfigurationKeys.AddUrl);
            public static string ApiUrl => Startup.StaticConfig.GetValue<string>(Constants.ConfigurationKeys.ApiUrl);
            public static string HostKey => Startup.StaticConfig.GetValue<string>(Constants.ConfigurationKeys.HostKey);
            public static string RedirectUrl => Startup.StaticConfig.GetValue<string>(Constants.ConfigurationKeys.RedirectUrl);
        }

        public static class FunctionUrls
        {
            
            public static string GetUrl(string name) {
                return $"{Configuration.ApiUrl}/GetUrl?code={Configuration.HostKey}&name={name}";
            }

            public static string AddUrl() {
                return $"{Configuration.ApiUrl}/AddUrl?code={Configuration.HostKey}";
            }
        }
    }
}