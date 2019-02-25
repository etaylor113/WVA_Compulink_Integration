﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.Response
{
    class ResponseData
    {
        [JsonProperty("order")]
        public OrderErrors OrderErrors { get; set; } 

        public string Wva_order_id { get; set; }
    }
}