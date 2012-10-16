using System.Web;

namespace WebTestHelper.Fakes
{
    public class FakeHttpContext : HttpContextBase
    {
        private FakeHttpRequest _request;

        public FakeHttpContext(string url)
        {
            _request = new FakeHttpRequest(url);
        }

        public override HttpRequestBase Request
        {
            get
            {
                return _request;
            }
        }
    }
}
