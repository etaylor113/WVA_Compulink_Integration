using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Views.Error;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace CompulinkIntegration.Tests
{
    public class ErrorWindowTests
    {
        [Theory]
        [InlineData("File not found exception", "File not found exception")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void ErrorWindow_StringErrorEqualsValue(string error, string expected)
        {
            Thread thread = new Thread(() =>
            {
                ErrorWindow errorWindow = new ErrorWindow(error);

                string actual = errorWindow.Error;

                Assert.Equal(expected, actual);
            });         
        }

    }
}
