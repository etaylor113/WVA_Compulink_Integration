using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models.Response;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;

namespace WVA_Compulink_Integration.ViewModels.Orders
{
    class OrderCreationViewModel
    {
        public static Order Order { get; set; }
        public static List<Prescription> Prescriptions { get; set; }
        public static string OrderName { get; set; }

        public OrderCreationViewModel()
        {

        }

        public OrderCreationViewModel(List<Prescription> listPrescriptions, string orderName)
        {
            Order = null;
            OrderName = orderName;

            // Deletes any orders without a product name if user has this setting enabled
            if (UserData.Data?.Settings != null && UserData.Data.Settings.DeleteBlankCompulinkOrders)
            {
                listPrescriptions.RemoveAll(p => p.Product == null);
                listPrescriptions.RemoveAll(p => p.Product.Trim() == "");
            }
            else
            {
                for (int i = 0; i < listPrescriptions.Count; i++)
                {
                    if (listPrescriptions[i].Product == null || listPrescriptions[i].Product.Trim() == "")
                    {
                        Prescription p = listPrescriptions.Where(x => x.FirstName == listPrescriptions[i].FirstName && x.LastName == listPrescriptions[i].LastName).First();
                    }
                }
            }

            Prescriptions = listPrescriptions;
        }

        public OrderCreationViewModel(Order order, string orderName)
        {
            Order = order;
            OrderName = OrderName;
        }

        public OrderCreationViewModel(Order order, List<Prescription> listPrescriptions,  string orderName)
        {
            Order = order;
            Prescriptions = listPrescriptions;
            OrderName = OrderName;
        }

        public static Order GetOrder(string orderName)
        {
            string dsn = UserData.Data.DSN;          
            string endpoint = $"http://{dsn}/api/order/exists/";
            string strOrder = API.Post(endpoint, orderName);
            var order = JsonConvert.DeserializeObject<Order>(strOrder);

            // Change shipping code to readable string value
            if (order != null)
                order.ShippingMethod = GetShippingString(order.ShippingMethod);

            return order;
        }

        public static OrderResponse CreateOrder(OutOrderWrapper outOrderWrapper)
        {
            string dsn = UserData.Data.DSN;
            string endpoint = $"http://{dsn}/api/order/submit/";
            string strResponse = API.Post(endpoint, outOrderWrapper);
            OrderResponse response = JsonConvert.DeserializeObject<OrderResponse>(strResponse);

            return response;
        }

        public static OrderResponse DeleteOrder(string orderName)
        {
            string dsn = UserData.Data.DSN;
            string endpoint = $"http://{dsn}/api/order/delete/";
            string strResponse = API.Post(endpoint, orderName);
            OrderResponse response = JsonConvert.DeserializeObject<OrderResponse>(strResponse);

            return response;
        }

        public static OrderResponse SaveOrder(OutOrderWrapper outOrderWrapper)
        {
            if (outOrderWrapper?.OutOrder?.PatientOrder?.OrderName == null || outOrderWrapper?.OutOrder?.PatientOrder?.OrderName.Trim() == "")
                return null;

            string dsn = UserData.Data.DSN;
            string endpoint = $"http://{dsn}/api/order/save/";
            string strResponse = API.Post(endpoint, outOrderWrapper);
            OrderResponse response = JsonConvert.DeserializeObject<OrderResponse>(strResponse);

            return response;
        }

        private static string GetShippingString(string shipID)
        {
            switch (shipID)
            {
                case "1":
                    return "Standard";
                case "D":
                    return "UPS Ground";
                case "J":
                    return "UPS 2nd Day Air";
                case "P":
                    return "UPS Next Day Air";
                default:
                    return shipID;
            }
        }

    }
}
