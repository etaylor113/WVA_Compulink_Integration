﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.Prescription
{
    public class PrescriptionWrapper
    {
        [JsonProperty("request")]
        public PrescriptionRequest Request { get; set; }
    }
}