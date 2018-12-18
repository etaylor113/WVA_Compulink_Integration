using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.ProductParameters.Parent
{
    public class Parameter
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("value")]
        public string Value;

        [JsonProperty("error_message")]
        public string ErrorMessage;

        [JsonProperty("is_valid")]
        public bool IsValid
        {
            get
            {
                if (ErrorMessage == null)
                    return true;
                else if (ErrorMessage.Trim() == "")
                    return true;
                else
                    return false;
            }
            set { }
        }
    }
}
