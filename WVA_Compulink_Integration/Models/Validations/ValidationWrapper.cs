﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.Validations
{
    class ValidationWrapper
    {
        [JsonProperty("request")]
        public ProductValidation Request { get; set; }
    }
}
