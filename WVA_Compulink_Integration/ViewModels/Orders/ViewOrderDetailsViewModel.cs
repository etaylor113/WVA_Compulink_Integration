using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WVA_Compulink_Integration.Models.Order.Out;

namespace WVA_Compulink_Integration.ViewModels.Orders
{
    public class ViewOrderDetailsViewModel
    {
        public static Order SelectedOrder;

        public ViewOrderDetailsViewModel(Order order)
        {
            SelectedOrder = order; 
        }
    }
}
