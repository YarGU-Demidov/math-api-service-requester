using System.Collections.Generic;

namespace MathSite.Common.ApiServiceRequester.Abstractions
{
    public class ServiceMethod
    {
        public ServiceMethod(string serviceName, string methodName)
        {
            MethodName = methodName;
            ServiceName = serviceName;
        }

        public string MethodName { get; set; }
        public string ServiceName { get; set; }
    }
}