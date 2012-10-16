using System;

namespace WebTestHelper.WebServer
{
    public interface IWebServer : IDisposable
    {
        string RootUrl { get; }

        void Start();
        void Stop();
    }
}
