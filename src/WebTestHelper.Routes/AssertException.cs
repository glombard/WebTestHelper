using System;

namespace WebTestHelper.Routes
{
    public class AssertException : Exception
    {
        public AssertException(string message)
            : base(message)
        {
        }
    }
}
