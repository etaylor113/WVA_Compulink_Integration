using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.OrderStatus.FromApi
{
    class Item
    {
        [JsonProperty("deleted_flag")]
        public string DeletedFlag { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("qty_backordered")]
        public int QuantityBackordered { get; set; }

        [JsonProperty("qty_cancelled")]
        public int QuantityCancelled { get; set; }

        [JsonProperty("qty_shipped")]
        public int QuantityShipped { get; set; }
    }
}
