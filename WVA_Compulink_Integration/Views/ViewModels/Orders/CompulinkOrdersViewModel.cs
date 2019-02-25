using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Prescription;

namespace WVA_Compulink_Integration.ViewModels.Orders
{
    class CompulinkOrdersViewModel
    {
        public static List<Prescription> ListPrescriptions = new List<Prescription>();

        public CompulinkOrdersViewModel()
        {

        }

        public CompulinkOrdersViewModel(List<Prescription> prescriptions)
        {
            ListPrescriptions.AddRange(prescriptions);
        }
    }
}
