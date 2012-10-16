namespace WebTestHelper.WebServer
{
    public interface IRemoteWebServer : IWebServer
    {
        void Initialize(string remoteUrl);
    }
}
