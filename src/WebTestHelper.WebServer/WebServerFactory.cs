using System;
using System.Collections.Generic;
using System.Linq;

namespace WebTestHelper.WebServer
{
    public static class WebServerFactory
    {
        private static readonly IDictionary<string, Func<IWebServer>> _webServerCreators;
        private static IWebServerSettings _webServerSettings;

        static WebServerFactory()
        {
            _webServerCreators = new Dictionary<string, Func<IWebServer>>();
            WebServerFactoryBootstrapper.Initialize();
        }

        public static void Initialize(IWebServerSettings webServerSettings)
        {
            _webServerSettings = webServerSettings;
        }

        public static IWebServer CreateWebServer()
        {
            if (_webServerSettings == null)
            {
                throw new WebServerException("WebServerFactory has not been initialized with settings yet");
            }
            return CreateWebServer(_webServerSettings.ServerType, _webServerSettings.RemoteUrl, _webServerSettings.WebProjectPath, _webServerSettings.Port);
        }

        public static IWebServer CreateWebServer(string serverType, string remoteUrl, string webProjectPath, int port)
        {
            string serverTypeName = _webServerCreators.Keys.FirstOrDefault(x => x.IndexOf(serverType,  StringComparison.InvariantCultureIgnoreCase) == 0);
            if (serverTypeName != null)
            {
                Func<IWebServer> instanceCreator = _webServerCreators[serverTypeName];
                IWebServer webServer = instanceCreator();
                if (webServer is ILocalWebServer)
                {
                    ((ILocalWebServer)webServer).Initialize(webProjectPath, port);
                }
                else if (webServer is IRemoteWebServer)
                {
                    ((IRemoteWebServer)webServer).Initialize(remoteUrl);
                }
                webServer.Start();
                return webServer;
            }
            else
            {
                throw new WebServerException(string.Format("No web server type '{0}' registered", serverType));
            }
        }

        public static void RegisterWebServer<T>() where T : IWebServer, new()
        {
            string serverType = typeof(T).Name;
            RegisterWebServer(serverType, () => new T());
        }

        public static void RegisterWebServer(string serverType, Func<IWebServer> webServerCreator)
        {
            _webServerCreators[serverType] = webServerCreator;
        }
    }
}
