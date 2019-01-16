using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Prescription;

namespace WVA_Compulink_Integration.ViewModels.Orders
{
    class WVAOrdersViewModel
    {
        public static List<Prescription> ListPrescriptions = new List<Prescription>();

        public WVAOrdersViewModel()
        {

        }

        public WVAOrdersViewModel(List<Prescription> prescriptions)
        {
            ListPrescriptions.AddRange(prescriptions);
        }
    }
}
