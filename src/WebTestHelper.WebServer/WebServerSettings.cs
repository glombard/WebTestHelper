using System;

namespace WebTestHelper.WebServer
{
    public class WebServerSettings : IWebServerSettings
    {
        public string ServerType { get; private set; }
        public string WebProjectPath { get; private set; }
        public string RemoteUrl { get; private set; }
        public int Port { get; private set; }

        public WebServerSettings(IConfigurationManager configurationManager, IWebProjectPathFinder webProjectPathFinder)
        {
            ServerType = configurationManager.AppSettings["webServer.serverType"];
            WebProjectPath = configurationManager.AppSettings["webServer.webProjectPath"];
            Port = Convert.ToInt32(configurationManager.AppSettings["webServer.port"]);
            RemoteUrl = configurationManager.AppSettings["webServer.remoteUrl"];

            if (string.IsNullOrWhiteSpace(RemoteUrl))
            {
                if (string.IsNullOrWhiteSpace(ServerType))
                {
                    ServerType = "IISExpress";
                }

                if (string.IsNullOrWhiteSpace(WebProjectPath))
                {
                    WebProjectPath = webProjectPathFinder.FindWebProjectPath();
                }

                if (Port == 0)
                {
                    Port = 8082;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(ServerType))
                {
                    ServerType = "Remote";
                }
            }
        }
    }
}
