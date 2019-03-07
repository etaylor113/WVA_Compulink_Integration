using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Error;
using Xunit;

namespace CompulinkIntegration.Tests
{
    public class ErrorOutputTests
    {
        [Theory]
        [InlineData("Evan", "File not found exception", "Evan", "File not found exception")]
        [InlineData(null, "File not found exception", null, "File not found exception")]
        [InlineData("Evan", null, "Evan",null)]
        [InlineData("", "File not found exception", "", "File not found exception")]
        [InlineData("Evan", "", "Evan", "")]
        public void ErrorOutputEqualsValues(string user, string error, string expectedUser, string expectedError)
        {
            ErrorOutput errorOutput = new ErrorOutput(user, error);

            string actualUser = errorOutput.User;
            string actualError = errorOutput.Error;

            Assert.Equal(expectedUser, actualUser);
            Assert.Equal(expectedError, actualError);
        }

    }
}
