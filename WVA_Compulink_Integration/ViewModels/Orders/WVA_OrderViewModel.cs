using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Prescription;

namespace WVA_Compulink_Integration.ViewModels.Orders
{
    class WVA_OrderViewModel
    {
        public static List<Prescription> ListPrescriptions = new List<Prescription>();

        public WVA_OrderViewModel()
        {

        }

        public WVA_OrderViewModel(List<Prescription> prescriptions)
        {
            ListPrescriptions.AddRange(prescriptions);
        }

        public static void SaveOrders()
        {
            // Push order (item detail items) to database with the order ID
        }

    }
}
