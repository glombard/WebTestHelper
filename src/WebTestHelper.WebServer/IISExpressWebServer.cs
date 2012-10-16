using System;
using System.Diagnostics;
using System.IO;

namespace WebTestHelper.WebServer
{
    public class IISExpressWebServer : ILocalWebServer
    {
        private string _webProjectPath;
        private int _port;
        private Process _process;

        public string RootUrl { get; private set; }

        public void Initialize(string webProjectPath, int port)
        {
            _webProjectPath = webProjectPath;
            _port = port;
            RootUrl = string.Format("http://localhost:{0}", port);
        }

        public void Start()
        {
            if (_process != null)
            {
                throw new WebServerException("IIS Express web server already started");
            }

            if (string.IsNullOrWhiteSpace(_webProjectPath))
            {
                throw new WebServerException("Web project path not initialized");
            }

            if (_port == 0)
            {
                throw new WebServerException("Port number not initialized");
            }

            string iisExpressFile = Environment.GetFolderPath(Environment.Is64BitOperatingSystem ? Environment.SpecialFolder.ProgramFiles : Environment.SpecialFolder.ProgramFilesX86);
            iisExpressFile = Path.Combine(iisExpressFile, @"IIS Express\iisexpress.exe");

            if (!File.Exists(iisExpressFile))
            {
                throw new WebServerException(string.Format("IIS Express executable not found: '{0}'", iisExpressFile));
            }

            _process = new Process();
            _process.StartInfo.FileName = iisExpressFile;
            _process.StartInfo.Arguments = string.Format("/port:{0} /path:\"{1}\"", _port, _webProjectPath);
            _process.Start();
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_process != null)
            {
                if (!_process.HasExited)
                {
                    _process.CloseMainWindow();
                }

                _process.Dispose();
                _process = null;
            }
        }
    }
}
