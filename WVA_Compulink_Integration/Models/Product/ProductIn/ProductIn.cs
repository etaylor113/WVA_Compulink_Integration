using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Response;

namespace WVA_Compulink_Integration.Models.Product.ProductIn
{
    class ProductIn : Response.Response
    {
        [JsonProperty("data")]
        public List<Product> Products { get; set; }
    }
}
