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
        public string QuantityBackordered { get; set; }

        [JsonProperty("qty_cancelled")]
        public string QuantityCancelled { get; set; }

        [JsonProperty("qty_shipped")]
        public string QuantityShipped { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("item_status")]
        public string ItemStatus { get; set; }

        [JsonProperty("tracking_number")]
        public string TrackingNumber { get; set; }

        [JsonProperty("tracking_url")]
        public string TrackingUrl { get; set; }

        [JsonProperty("shipping_date")]
        public string ShippingDate { get; set; }

        [JsonProperty("shipping_carrier")]
        public string ShippingCarrier { get; set; }
    }
}
