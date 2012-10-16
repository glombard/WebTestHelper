namespace WebTestHelper.WebServer
{
    public class RemoteWebServer : IRemoteWebServer
    {
        public string RootUrl { get; private set; }

        public void Initialize(string remoteUrl)
        {
            RootUrl = remoteUrl;
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        public void Dispose()
        {
        }
    }
}
