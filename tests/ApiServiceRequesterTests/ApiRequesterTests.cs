using System.Net;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester;
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

        public ApiRequesterTests()
        {
            var testCookie = new Cookie(CookieName, CookieValue, CookiePath, CookieDomain);
            var testCookieRetriever = new TestCookieRetriever(testCookie);
            _requester = new ApiRequester(new ExactUrlServiceUrlBuilder(), testCookieRetriever);
        }

        [Fact]
        public async Task CorrectRequest()
        {
            var result = await _requester.GetAsync<bool>(new TestApiEndpoint("true"), "/");

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
            var result = await _requester.GetAsync<TestClassWithSomeData>(testApiEndpoint, path: "/");

            Assert.NotNull(result);
            Assert.Equal("test valaue", result.Data);
            Assert.Equal(true, result.BoolData);
            Assert.Equal(123, result.NullableInt);
            Assert.Equal(null, result.EmptyNullableDecimal);

            var gotCookie = testApiEndpoint.GivenCoockie;
            
            Assert.Equal(CookieName, gotCookie.Name);
            Assert.Equal(CookieValue, gotCookie.Value);
            Assert.Equal(CookiePath, gotCookie.Path);
            Assert.Equal(CookieDomain, gotCookie.Domain);
        }

        [Fact]
        public async Task CookieWasSetCorrectly()
        {
            var testApiEndpoint = new TestApiEndpoint(JsonConvert.SerializeObject(true));
            await _requester.GetAsync<bool>(testApiEndpoint, path: "/");

            var gotCookie = testApiEndpoint.GivenCoockie;
            
            Assert.Equal(CookieName, gotCookie.Name);
            Assert.Equal(CookieValue, gotCookie.Value);
            Assert.Equal(CookiePath, gotCookie.Path);
            Assert.Equal(CookieDomain, gotCookie.Domain);
        }

        [Fact]
        public async Task CookieWasNotSet()
        {
            var testCookieRetriever = new TestCookieRetriever(null);
            _requester = new ApiRequester(new ExactUrlServiceUrlBuilder(), testCookieRetriever);
            
            var testApiEndpoint = new TestApiEndpoint(JsonConvert.SerializeObject(true));
            await _requester.GetAsync<bool>(testApiEndpoint, path: "/");

            var gotCookie = testApiEndpoint.GivenCoockie;
            
            Assert.Null(gotCookie);
        }
    }
}