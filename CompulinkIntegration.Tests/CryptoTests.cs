using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Cryptography;
using Xunit;

namespace CompulinkIntegration.Tests
{
    public class CryptoTests
    {
        [Theory]
        [InlineData("Evant123", "daf8ae299773ee1b8000e6507b3ad64bc0620ac6a0446cd7c247f7b36267172f")]
        [InlineData("Evanisgreat123456", "134054b4df639afa9f278ef13bbbf9c563554a6aa7cf347f8f226fa80e9d1ea8")]
        [InlineData("", null)]
        [InlineData("    ", null)]
        [InlineData(null, null)]
        [InlineData("eeeeeeeeeeeeeeeeeeee", "d9b2d2949514173db9579963ff308d57dd98b827fd266bc657cfee6f8d064a70")]
        public void ConvertToHashReturnsValue(string inputString, string expected)
        {
            string actual = Crypto.ConvertToHash(inputString);

            Assert.Equal(expected, actual);  
        }

        [Theory]
        [InlineData("e", "'inputString' must be at least 6 characters")]
        [InlineData("ee", "'inputString' must be at least 6 characters")]
        [InlineData("eee", "'inputString' must be at least 6 characters")]
        [InlineData("eeee", "'inputString' must be at least 6 characters")]
        [InlineData("eeeee", "'inputString' must be at least 6 characters")]
        public void ConvertToHashThrowsException(string inputString, string expected)
        {
            Exception actual = Assert.Throws<Exception>(() =>  Crypto.ConvertToHash(inputString));

            Assert.Equal(expected, actual.Message);
        }
    }
}
