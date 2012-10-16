namespace WebTestHelper.WebServer
{
    internal static class WebServerFactoryBootstrapper
    {
        internal static void Initialize()
        {
            IConfigurationManager configurationManager = new ConfigurationManagerWrapper();
            IWebProjectPathFinder webProjectPathFinder = new WebProjectPathFinder();
            IWebServerSettings webServerSettings = new WebServerSettings(configurationManager, webProjectPathFinder);
            WebServerFactory.Initialize(webServerSettings);
            WebServerFactory.RegisterWebServer<IISExpressWebServer>();
            WebServerFactory.RegisterWebServer<RemoteWebServer>();
        }
    }
}
