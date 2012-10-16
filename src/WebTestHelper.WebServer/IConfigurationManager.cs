using System.Collections.Specialized;

namespace WebTestHelper.WebServer
{
    public interface IConfigurationManager
    {
        NameValueCollection AppSettings { get; }
    }
}