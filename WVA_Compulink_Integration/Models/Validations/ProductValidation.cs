using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Order;

namespace WVA_Compulink_Integration.Models.Validations
{
    class ProductValidation
    {
        [JsonProperty("api_key")]
        public string Key { get; set; }

        [JsonProperty("products")]
        public List<ItemDetail> ProductsToValidate { get; set; }
    }
}
