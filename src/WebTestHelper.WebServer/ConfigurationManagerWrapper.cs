using System.Collections.Specialized;
using System.Configuration;

namespace WebTestHelper.WebServer
{
    public class ConfigurationManagerWrapper : IConfigurationManager
    {
        public NameValueCollection AppSettings { get { return ConfigurationManager.AppSettings; } }
    }
}
