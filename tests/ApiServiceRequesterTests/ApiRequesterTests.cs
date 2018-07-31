using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester;
using MathSite.Common.ApiServiceRequester.Abstractions;
using MathSite.Common.ApiServiceRequester.UriBuilders;
using Newtonsoft.Json;
using Xunit;

namespace ApiServiceRequesterTests
{
    public class TestClassWithSomeData
    {
        public string Data { get; set; }
        public bool BoolData { get; set; }
        public int? NullableInt { get; set; }
        public decimal? EmptyNullableDecimal { get; set; }
    }

    public class ApiRequesterTests
    {

        private ApiRequester _requester;
        private const string CookieName = "TestKey";
        private const string CookieValue = "TestValue";
        private const string CookiePath = "";
        private const string CookieDomain = "";

        [Fact]
        public async Task CookieWasNotSet()
        {
            var testCookieRetriever = new TestAuthDataRetriever(null);
            var testApiEndpoint = new TestApiEndpoint(JsonConvert.SerializeObject(true));
            SetRequester(testApiEndpoint, testCookieRetriever);

            await _requester.GetAsync<bool>(new ServiceMethod("a", "b"), null);

            var gotCookie = testApiEndpoint.GivenCoockie;

            Assert.Null(gotCookie);
        }

        [Fact]
        public async Task CookieWasSetCorrectly()
        {
            var testApiEndpoint = new TestApiEndpoint(JsonConvert.SerializeObject(true));
            SetRequester(testApiEndpoint);
            
            await _requester.GetAsync<bool>(new ServiceMethod("a", "b"), null);

            var gotCookie = testApiEndpoint.GivenCoockie;

            Assert.Equal(CookieName, gotCookie.Name);
            Assert.Equal(CookieValue, gotCookie.Value);
            Assert.Equal(CookiePath, gotCookie.Path);
            Assert.Equal(CookieDomain, gotCookie.Domain);
        }

        [Fact]
        public async Task CorrectRequest()
        {
            SetRequester(new TestApiEndpoint(JsonConvert.SerializeObject(true)));
            var result = await _requester.GetAsync<bool>(new ServiceMethod("a", "b"), null);

            Assert.True(result);
        }

        [Fact]
        public async Task CorrectUploadRequest()
        {
            var bytes = new byte[] {200, 0, 1, 2, 3};

            var endpoint = new TestApiEndpoint(JsonConvert.SerializeObject(true));
            SetRequester(endpoint);
            var result = await _requester.SendDataAsync<bool>(new ServiceMethod("a", "b"), new MemoryStream(bytes));
            var expected = new List<byte>(bytes).ToArray();

            Assert.Equal(expected, await endpoint.GivenData.ReadAsByteArrayAsync());
            Assert.True(result);
        }

        [Fact]
        public async Task CorrectRequestWithCustomClass()
        {
            var expected = new TestClassWithSomeData
            {
                Data = "test valaue",
                BoolData = true,
                NullableInt = 123,
                EmptyNullableDecimal = null
            };
            var testApiEndpoint = new TestApiEndpoint(JsonConvert.SerializeObject(expected));
            SetRequester(testApiEndpoint);
            var result = await _requester.GetAsync<TestClassWithSomeData>(new ServiceMethod("a", "b"), null);

            Assert.NotNull(result);
            Assert.Equal("test valaue", result.Data);
            Assert.True(result.BoolData);
            Assert.Equal(123, result.NullableInt);
            Assert.Null(result.EmptyNullableDecimal);

            var gotCookie = testApiEndpoint.GivenCoockie;

            Assert.Equal(CookieName, gotCookie.Name);
            Assert.Equal(CookieValue, gotCookie.Value);
            Assert.Equal(CookiePath, gotCookie.Path);
            Assert.Equal(CookieDomain, gotCookie.Domain);
        }

        private void SetRequester(IApiEndpoint apiEndpoint, IAuthDataRetriever authDataRetriever = null)
        {
            var testCookie = new AuthData
            {
                CookieKey = CookieName,
                CookieValue = CookieValue,
                CookiePath = CookiePath,
                CookieDomain = CookieDomain
            };

            var testCookieRetriever = authDataRetriever ?? new TestAuthDataRetriever(testCookie);
            _requester = new ApiRequester(new ExactUrlServiceUrlBuilder(), testCookieRetriever, new TestApiEndpiontFactory(apiEndpoint));
        }
    }
}