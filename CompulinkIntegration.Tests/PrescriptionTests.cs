using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Prescription;
using Xunit;

namespace CompulinkIntegration.Tests
{
    public class PrescriptionTests
    {
        [Theory]
        [InlineData("Evan", "Taylor", "Taylor, Evan")]
        [InlineData(null, "Taylor", "Taylor, ")]
        [InlineData("Evan", null, ", Evan")]
        public void PatientNameEqualsValue(string firstName, string lastName, string expected)
        {
            Prescription patient = new Prescription
            {
                FirstName = firstName,
                LastName = lastName
            };

            string actual = patient.Patient;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PatientIsCheckedIsTrue()
        {
            Prescription prescription = new Prescription();

            bool expected = false;
            bool actual = prescription.IsShipToPatient;

            Assert.Equal(expected, actual);
        }

    }
}
