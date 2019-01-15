using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WVA_Compulink_Integration.Models.Patient
{
    public class Patient
    {
        [JsonProperty("patientID")]
        public string PatientID { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
   
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        private string fullName;

        public string FullName
        {
            get {
                return $"{LastName} {FirstName}";
            }
            set { fullName = value; }
        }

    }
}
