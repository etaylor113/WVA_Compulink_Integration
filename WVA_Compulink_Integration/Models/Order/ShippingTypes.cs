using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WVA_Compulink_Integration.Models.Order
{
    class ShippingTypes
    {
        public static List<string> ListShippingTypes = new List<string>()
        {
            "Free Standard",
            "Standard",
            "UPS Ground",
            "UPS 2nd Day Air",
            "UPS Next Day Air",
        };
   
    }
}
