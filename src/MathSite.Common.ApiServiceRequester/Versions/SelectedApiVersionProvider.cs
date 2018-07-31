using System;
using MathSite.Common.ApiServiceRequester.Abstractions;

namespace MathSite.Common.ApiServiceRequester.Versions
{
    public class SelectedApiVersionProvider : IApiVersionProvider
    {
        private string _version;

        public SelectedApiVersionProvider(string version)
        {
            SetVersion(version);
        }
        
        public SelectedApiVersionProvider(Version version)
        {
            SetVersion(version);
        }

        public void SetVersion(string version)
        {
            // version 1.0 should look like 1,
            // 1.2.0 -> 1.2, etc.
            _version = version.Replace(".0", "");
        }
        
        public void SetVersion(Version version)
        {
            SetVersion(version.ToString());
        }
        
        public string GetVersion()
        {
            return _version;
        }
    }
}