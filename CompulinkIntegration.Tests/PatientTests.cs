using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Patient;
using Xunit;

namespace CompulinkIntegration.Tests
{
    public class PatientTests
    {
        [Theory]
        [InlineData("Evan", "Taylor", "Taylor Evan")]
        [InlineData(null, "Taylor", "Taylor ")]
        [InlineData("Evan", null, " Evan")]
        public void FullNameReturnsLastNamePlusFirstName(string firstName, string lastName, string expected)
        {
            Patient patient = new Patient
            {
                FirstName = firstName,
                LastName = lastName
            };

            string actual = patient.FullName;

            Assert.Equal(expected, actual);
        }

    }
}
