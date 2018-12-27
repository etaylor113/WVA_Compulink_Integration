using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.Order.Out
{
    class OutOrderWrapper
    {
        [JsonProperty("request")]
        public OutOrder OutOrder { get; set; }
    }
}
