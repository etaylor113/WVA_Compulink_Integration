using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration._API;
using WVA_Compulink_Integration.Memory;
using WVA_Compulink_Integration.Models;
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
            Prescriptions = listPrescriptions;
            OrderName = orderName;
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
            string endpoint = "http://localhost:56075/CompuClient/orders/exists/";
            string strOrder = API.Post(endpoint, orderName);
            Order order = JsonConvert.DeserializeObject<Order>(strOrder);

            return order;
        }

        public static Response CreateOrder(OutOrderWrapper outOrderWrapper)
        {
            string endpoint = "http://localhost:56075/CompuClient/orders/submit/";
            string strResponse = API.Post(endpoint, outOrderWrapper);
            Response response = JsonConvert.DeserializeObject<Response>(strResponse);

            return response;
        }

        public static Response DeleteOrder(string orderName)
        {
            string endpoint = "http://localhost:56075/CompuClient/orders/delete/";
            string strResponse = API.Post(endpoint, orderName);
            Response response = JsonConvert.DeserializeObject<Response>(strResponse);

            return response;
        }

        public static Response SaveOrder(OutOrderWrapper outOrderWrapper)
        {
            string endpoint = "http://localhost:56075/CompuClient/orders/save/";
            string strResponse = API.Post(endpoint, outOrderWrapper);
            Response response = JsonConvert.DeserializeObject<Response>(strResponse);

            return response;
        }

    }
}
