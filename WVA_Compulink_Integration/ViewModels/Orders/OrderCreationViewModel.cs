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

        public OrderCreationViewModel()
        {

        }

        public OrderCreationViewModel(List<Prescription> listPrescriptions, string orderName = "")
        {
            Prescriptions = listPrescriptions;
        }

        public OrderCreationViewModel(Order order, List<Prescription> listPrescriptions,  string orderName)
        {
            Order = order;
            Prescriptions = listPrescriptions;
        }

    }
}
