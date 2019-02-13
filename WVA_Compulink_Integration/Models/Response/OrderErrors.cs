using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.Response
{
    class OrderErrors
    {
        [JsonProperty("errors")]
        public List<string> ErrorMessages { get; set; }
    }
}
