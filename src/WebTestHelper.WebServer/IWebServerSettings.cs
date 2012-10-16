namespace WebTestHelper.WebServer
{
    public interface IWebServerSettings
    {
        string ServerType { get; }
        string WebProjectPath { get; }
        string RemoteUrl { get; }
        int Port { get;  }
    }
}
