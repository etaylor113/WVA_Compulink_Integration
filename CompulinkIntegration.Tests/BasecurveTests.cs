using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.ProductParameters;
using Xunit;

namespace CompulinkIntegration.Tests
{
    public class BasecurveTests
    {
        [Theory]
        [InlineData("8.6", null, true)]
        [InlineData("8.6", "   ", true)]
        [InlineData("8.6", "", true)]
        [InlineData("8.6", "error message", false)]
        public void BasecurveIsValidEqualsTrue (string value, string errorMessage, bool expected)
        {
            BaseCurve baseCurve = new BaseCurve
            {
                Value = value,
                ErrorMessage = errorMessage
            };

            bool actual = baseCurve.IsValid;

            Assert.Equal(expected, actual);
        }

    }
}
