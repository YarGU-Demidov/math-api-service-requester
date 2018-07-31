﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MathSite.Common.ApiServiceRequester.Abstractions;

namespace ApiServiceRequesterTests
{
    public class TestApiEndpoint : IApiEndpoint
    {
        private readonly string _value;

        public TestApiEndpoint(string value)
        {
            _value = value;
        }

        public Cookie GivenCoockie { get; set; }
        public HttpContent GivenData { get; set; }

        public async Task<HttpResponseMessage> GetAsync(string path, Cookie authCookie,
            IServiceUriBuilder serviceUriBuilder)
        {
            GivenCoockie = authCookie;
            GivenData = null;
            return new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(_value)};
        }

        public async Task<HttpResponseMessage> PostAsync(string path, HttpContent data, Cookie authCookie,
            IServiceUriBuilder serviceUriBuilder)
        {
            GivenCoockie = authCookie;
            GivenData = data;
            return new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(_value)};
        }
    }
}