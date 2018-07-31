using System;
using MathSite.Common.ApiServiceRequester.Versions;
using Xunit;

namespace VersionsTests
{
    public class SelectedVersionsTests
    {
        [Fact]
        public void CreateWithVersionTest()
        {
            var ver = new Version(1, 0);
            var provider = new SelectedApiVersionProvider(ver);

            var expected = ver.ToString().Replace(".0", "");

            Assert.Equal(expected, provider.GetVersion());
        }

        [Fact]
        public void CreateWithComplicatedVersionTest()
        {
            var ver = new Version(2, 3, 150, 17);
            var provider = new SelectedApiVersionProvider(ver);

            var expected = ver.ToString();

            Assert.Equal(expected, provider.GetVersion());
        }

        [Fact]
        public void CreateWithStringTest()
        {
            const string ver = "1.0";
            var provider = new SelectedApiVersionProvider(ver);

            var expected = ver.Replace(".0", "");

            Assert.Equal(expected, provider.GetVersion());
        }

        [Fact]
        public void CreateWithComplicatedStringTest()
        {
            const string ver = "2.3.0-alpha"; // -> 2.3-alpha
            var provider = new SelectedApiVersionProvider(ver);

            var expected = ver.Replace(".0", "");

            Assert.Equal(expected, provider.GetVersion());
        }
    }
}