using MathSite.Common.ApiServiceRequester.Versions;
using Xunit;

namespace VersionsTests
{
    public class AnyVersionsTests
    {
        [Fact]
        public void ShouldReturnNull()
        {
            string expected = null;
            string actual = new AnyApiVersionProvider().GetVersion();

            Assert.Equal(expected, actual);
        }
    }
}
