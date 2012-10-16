using System;

namespace WebTestHelper.WebServer
{
    public class WebServerException : Exception
    {
        public WebServerException(string message)
            : base(message)
        {
        }
    }
}
