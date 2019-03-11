using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Patient;
using WVA_Compulink_Integration.Models.Prescription;

namespace WVA_Compulink_Integration.ViewModels
{
    public class OrdersViewModel
    {
        public static string SelectedView { get; set; }
        public static List<Prescription> ListPrescriptions { get; set; }

        public OrdersViewModel()
        {

        }

        public OrdersViewModel(string selectedView, List<Prescription> listPrescriptions)
        {
            SelectedView = selectedView;
            ListPrescriptions = listPrescriptions;
        }
    }
}
