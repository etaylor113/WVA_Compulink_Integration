using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Prescription;

namespace WVA_Compulink_Integration.Memory
{
    class Orders
    {
        public static List<Prescription> CompulinkOrders = new List<Prescription>() { };
        public static List<Order> WVAOrders = new List<Order>() { };
    }
}
