using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace MathSite.Common.ApiServiceRequester.Abstractions.Exceptions
{
    [Serializable]
    public class RequestFailedException : ApplicationException
    {
        public RequestFailedException()
        {
        }

        protected RequestFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RequestFailedException(string message) : base(message)
        {
        }

        public RequestFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RequestFailedException(HttpResponseMessage response)
        {
            Response = response;
        }

        public RequestFailedException(string message, HttpResponseMessage response) : this(message)
        {
            Response = response;
        }

        public RequestFailedException(string message, Exception innerException, HttpResponseMessage response) : this(
            message, innerException)
        {
            Response = response;
        }

        public HttpResponseMessage Response { get; }
    }
}