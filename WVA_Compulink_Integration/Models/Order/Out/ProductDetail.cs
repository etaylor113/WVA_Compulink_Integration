﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.ProductParameters;

namespace WVA_Compulink_Integration.Models.Order.Out
{
    class ProductDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("wva_sku")]
        public SKU _SKU { get; set; }

        [JsonProperty("product_key")]
        public ProductKey _ProductKey { get; set; }

        [JsonProperty("upc")]
        public UPC _UPC { get; set; }

        [JsonProperty("bc")]
        public BaseCurve _BaseCurve { get; set; }

        [JsonProperty("dia")]
        public Diameter _Diameter { get; set; }

        [JsonProperty("sph")]
        public Sphere _Sphere { get; set; }

        [JsonProperty("cyl")]
        public Cylinder _Cylinder { get; set; }

        [JsonProperty("ax")]
        public Axis _Axis { get; set; }

        [JsonProperty("add")]
        public Add _Add { get; set; }

        [JsonProperty("color")]
        public Color _Color { get; set; }

        [JsonProperty("multifocal")]
        public Multifocal _Multifocal { get; set; }
    }
}
