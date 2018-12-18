using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;

namespace WVA_Compulink_Integration.ViewModels.Orders
{
    public class ShipToOfficeViewModel
    {
        public static List<Prescription> ListPrescriptions = new List<Prescription>();

        public ShipToOfficeViewModel()
        {

        }

        public ShipToOfficeViewModel(List<Prescription> prescriptions)
        {
            // Here you would post this data to db as well and synch the list with their cart
            //

            ListPrescriptions.AddRange(prescriptions);
        }
      
    }
}
