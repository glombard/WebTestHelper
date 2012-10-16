using WatiN.Core;

namespace WebTestHelper.WebServer.Sample.Tests
{
    public class IndexPage : Page
    {
        [FindBy(Id = "message")]
        public Para Message { get; private set; }
    }
}
