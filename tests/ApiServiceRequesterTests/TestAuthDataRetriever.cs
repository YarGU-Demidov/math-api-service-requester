using System.Net;
using MathSite.Common.ApiServiceRequester.Abstractions;

namespace ApiServiceRequesterTests
{
    public class TestAuthDataRetriever : IAuthDataRetriever
    {
        private readonly AuthData _authData;

        public TestAuthDataRetriever(AuthData authData)
        {
            _authData = authData;
        }

        public AuthData GetAuthData()
        {
            return _authData;
        }
    }
}