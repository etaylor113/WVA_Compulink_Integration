using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Prescriptions = listPrescriptions;
            OrderName = orderName;
        }

        public OrderCreationViewModel(Order order, List<Prescription> listPrescriptions,  string orderName)
        {
            Order = order;
            Prescriptions = listPrescriptions;
            OrderName = OrderName;
        }

        public static Order GetOrder(string orderName)
        {
            // Check if the given order exists
            try
            {
                string endpoint = "http://localhost:56075/CompuClient/orders/exists/";
                string strOrder = API.Post(endpoint, orderName);
                Order order = JsonConvert.DeserializeObject<Order>(strOrder);
                return order;
            }
            catch
            {
                return null;
            }
        }

        public static bool CreateOrder()
        {

        }




    }
}
