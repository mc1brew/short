using Microsoft.Extensions.Configuration;

namespace Short
{
    public static class Constants
    {
        public readonly static string Favicon = "favicon.ico";
        public static class ConfigurationKeys
        {
            public readonly static string CreateLinkPageUrl = "Shorter:CreateLinkPageUrl";
            public readonly static string DataStoreApiUrl = "Shorter:DataStoreApiUrl";
            public readonly static string DataStoreHostKey = "Shorter:DataStoreHostKey";
            public readonly static string ForwardLinkUrl = "Shorter:ForwardLinkUrl";
        }

        public static class Configuration
        {
            public static string CreateLinkPageUrl => Startup.StaticConfig.GetValue<string>(Constants.ConfigurationKeys.CreateLinkPageUrl);
            public static string DataStoreApiUrl => Startup.StaticConfig.GetValue<string>(Constants.ConfigurationKeys.DataStoreApiUrl);
            public static string DataStoreHostkey => Startup.StaticConfig.GetValue<string>(Constants.ConfigurationKeys.DataStoreHostKey);
            public static string ForwardLinkUrl => Startup.StaticConfig.GetValue<string>(Constants.ConfigurationKeys.ForwardLinkUrl);
        }

        public static class FunctionUrls
        {
            
            public static string GetUrl(string name) {
                return $"{Configuration.DataStoreApiUrl}/GetLink?code={Configuration.DataStoreHostkey}&name={name}";
            }

            public static string AddUrl() {
                return $"{Configuration.DataStoreApiUrl}/CreateLink?code={Configuration.DataStoreHostkey}";
            }
        }
    }
}