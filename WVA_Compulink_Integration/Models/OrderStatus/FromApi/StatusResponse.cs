using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.OrderStatus.FromApi
{
    class StatusResponse
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("messasge")]
        public string Message { get; set; }

        [JsonProperty("deleted_flag")]
        public string DeletedFlag { get; set; }

        [JsonProperty("shipping_status")]
        public string ShippingStatus { get; set; }

        [JsonProperty("tracking_number")]
        public string TrackingNumber { get; set; }

        [JsonProperty("tracking_url")]
        public string TrackingUrl { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }


    }
}
