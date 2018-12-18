using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Product.ProductOut;

namespace WVA_Compulink_Integration.Models.Out_Request
{
    public class Out_Request
    {
        [JsonProperty("request")]
        public ProductOut Request { get; set; }
    }
}
