using System.Web;

namespace WebTestHelper.Fakes
{
    public class FakeHttpRequest : HttpRequestBase
    {
        private string _url;

        public FakeHttpRequest(string url)
        {
            _url = url;
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get
            {
                return _url;
            }
        }

        public override string PathInfo
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
