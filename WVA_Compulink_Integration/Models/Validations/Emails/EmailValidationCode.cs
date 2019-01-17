using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.Validations.Emails
{
    class EmailValidationCode 
    {
        [JsonProperty("email_code")]
        public string EmailCode { get; set; }

        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
    }
}
