using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.Order.Out
{
    class Order
    {
        [JsonProperty("customer_id")]
        public string CustomerID { get; set; }

        [JsonProperty("order_name")]
        public string OrderName { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("dob")]
        public string DoB { get; set; }

        [JsonProperty("name_1")]
        public string Name_1 { get; set; }

        [JsonProperty("name_2")]
        public string Name_2 { get; set; }

        [JsonProperty("street_address_1")]
        public string StreetAddr_1 { get; set; }

        [JsonProperty("street_address_2")]
        public string StreetAddr_2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("ship_to_account")]
        public string ShipToAccount { get; set; }

        [JsonProperty("office_name")]
        public string OfficeName { get; set; }

        [JsonProperty("ordered_by")]
        public string OrderedBy { get; set; }

        [JsonProperty("po_number")]
        public string PoNumber { get; set; }

        [JsonProperty("shipping_method")]
        public string ShippingMethod { get; set; }

        [JsonProperty("ship_to_patient")]
        public string ShipToPatient { get; set; }

        [JsonProperty("freight")]
        public string Freight { get; set; }

        [JsonProperty("tax")]
        public string Tax { get; set; }

        [JsonProperty("discount")]
        public string Discount { get; set; }

        [JsonProperty("invoice_total")]
        public string InvoiceTotal { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}
