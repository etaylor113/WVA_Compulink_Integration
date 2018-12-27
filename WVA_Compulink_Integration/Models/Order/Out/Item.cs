using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.Order.Out
{
    class Item
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("patient_firstname")]
        public string FirstName { get; set; }

        [JsonProperty("patient_lastname")]
        public string LastName { get; set; }

        [JsonProperty("patient_id")]
        public int PatientID { get; set; }

        [JsonProperty("eye")]
        public string Eye { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("item_retail_price")]
        public double ItemRetailPrice { get; set; }

        [JsonProperty("product")]
        public ItemDetail ItemDetail { get; set; }
    }
}
