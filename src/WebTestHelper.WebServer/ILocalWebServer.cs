namespace WebTestHelper.WebServer
{
    public interface ILocalWebServer : IWebServer
    {
        void Initialize(string webProjectPath, int port);
    }
}
