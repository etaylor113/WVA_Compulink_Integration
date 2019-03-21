using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.ViewModels.Orders;

namespace WVA_Compulink_Integration.Views.Orders
{
    /// <summary>
    /// Interaction logic for ViewOrderDetails.xaml
    /// </summary>
    public partial class ViewOrderDetails : UserControl
    {
        public ViewOrderDetails()
        {
            InitializeComponent();
            SetUp();
        }

        private void SetUp()
        {
            var o = ViewOrderDetailsViewModel.SelectedOrder;

            // Header
            OrderNameLabel.Content  = o.OrderName;
            OrderedByLabel.Content  = o.OrderedBy;
            AccountIDLabel.Content  = o.CustomerID;

            // Sub-header
            // If value is not null or blank, add it to a stack panel column so the view scales smoothly
            if (o.Name_1 != null && o.Name_1.Trim() != "")
                LeftInnerStackPanel.Children.Add(new Label()
                {
                    Content = $"Addressee: {o.Name_1}",
                    FontFamily = new FontFamily("Sitka Text"),
                    FontSize = 16
                });

            if (o.StreetAddr_1 != null && o.StreetAddr_1.Trim() != "")
                LeftInnerStackPanel.Children.Add(new Label()
                {
                    Content = $"Address: {o.StreetAddr_1}",
                    FontFamily = new FontFamily("Sitka Text"),
                    FontSize = 16
                });

            if (o.ShippingMethod != null && o.ShippingMethod.Trim() != "")
                LeftInnerStackPanel.Children.Add(new Label()
                {
                    Content = $"Ship Type: {o.ShippingMethod}",
                    FontFamily = new FontFamily("Sitka Text"),
                    FontSize = 16
                });

            if (o.Name_1 != null && o.Phone.Trim() != "")
                LeftInnerStackPanel.Children.Add(new Label()
                {
                    Content = $"Phone: {o.Phone}",
                    FontFamily = new FontFamily("Sitka Text"),
                    FontSize = 16
                });

            if (o.City != null && o.City.Trim() != "")
                RightInnerStackPanel.Children.Add(new Label()
                {
                    Content = $"City: {o.City}",
                    FontFamily = new FontFamily("Sitka Text"),
                    FontSize = 16
                });

            if (o.Name_1 != null && o.State.Trim() != "")
                RightInnerStackPanel.Children.Add(new Label()
                {
                    Content = $"State: {o.State}",
                    FontFamily = new FontFamily("Sitka Text"),
                    FontSize = 16
                });

            if (o.Zip != null && o.Zip.Trim() != "")
                RightInnerStackPanel.Children.Add(new Label()
                {
                    Content = $"Zip: {o.Zip}",
                    FontFamily = new FontFamily("Sitka Text"),
                    FontSize = 16
                });

            if (o.StreetAddr_2 != null && o.StreetAddr_2.Trim() != "")
                RightInnerStackPanel.Children.Add(new Label()
                {
                    Content = $"Suite/Apt: {o.StreetAddr_2}",
                    FontFamily = new FontFamily("Sitka Text"),
                    FontSize = 16
                });

            // Grid items
            foreach (Item item in ViewOrderDetailsViewModel.SelectedOrder.Items)
            {
                ReviewOrderDataGrid.Items.Add(new Prescription()
                {
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Eye = item.Eye,
                    Product = item.ProductDetail.Name,
                    Quantity = item.Quantity,
                    BaseCurve = item.ProductDetail.BaseCurve,
                    Diameter = item.ProductDetail.Diameter,
                    Sphere = item.ProductDetail.Sphere,
                    Cylinder = item.ProductDetail.Cylinder,
                    Axis = item.ProductDetail.Axis,
                    Add = item.ProductDetail.Add,
                    Color = item.ProductDetail.Color,
                    Multifocal = item.ProductDetail.Multifocal
                });
            }
        }


    }
}
