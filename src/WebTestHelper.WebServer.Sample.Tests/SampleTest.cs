using WatiN.Core;
using Xunit;

namespace WebTestHelper.WebServer.Sample.Tests
{
    public class SampleTest
    {
        [Fact]
        public void Sample_app_started_in_IISExpress()
        {
            using (IWebServer server = WebServerFactory.CreateWebServer())
            using (Browser browser = new IE(server.RootUrl))
            {
                browser.WaitForComplete();
                IndexPage page = browser.Page<IndexPage>();
                Assert.Contains("Please run the WatiN test", page.Message.Text);
            }
        }

        [Fact]
        public void Remote_server_does_nothing()
        {
            using (IWebServer server = WebServerFactory.CreateWebServer("Remote", "http://www.bing.com/", null, 0))
            using (Browser browser = new IE(server.RootUrl))
            {
                browser.WaitForComplete();
                Assert.Equal("Bing", browser.Title);
            }
        }
    }
}
