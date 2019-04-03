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
        public int SelectedRow { get; set; }

        public ViewOrderDetails()
        {
            InitializeComponent();
            SetUp();
        }

        private void SetUp()
        {
            var o = ViewOrderDetailsViewModel.SelectedOrder;

            // Header
            OrderNameLabel.Content = o.OrderName;
            OrderedByLabel.Content = o.OrderedBy;
            AccountIDLabel.Content = o.CustomerID;
            OrderIDLabel.Content = $"WVA Order ID: {o.WvaStoreID}";

            // Sub-header (if value is not null or blank, add it to a stack panel column so the view scales smoothly)
            if (o.Name_1 != null && o.Name_1.Trim() != "")
                StackPanelAddLeftChild($"Addressee: {o.Name_1}");

            if (o.StreetAddr_1 != null && o.StreetAddr_1.Trim() != "")
                StackPanelAddLeftChild($"Address: {o.StreetAddr_1}");

            if (o.ShippingMethod != null && o.ShippingMethod.Trim() != "")
                StackPanelAddLeftChild($"Ship Type: {o.ShippingMethod}");

            if (o.Phone != null && o.Phone.Trim() != "")
                StackPanelAddLeftChild($"Phone: {o.Phone}");

            if (o.City != null && o.City.Trim() != "")
                StackPanelAddRightChild($"City: {o.City}");

            if (o.State != null && o.State.Trim() != "")
                StackPanelAddRightChild($"State: {o.State}");

            if (o.Zip != null && o.Zip.Trim() != "")
                StackPanelAddRightChild($"Zip: {o.Zip}");

            if (o.StreetAddr_2 != null && o.StreetAddr_2.Trim() != "")
                StackPanelAddRightChild($"Suite/Apt: {o.StreetAddr_2}");

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

        private void StackPanelAddLeftChild(string content)
        {
            LeftInnerStackPanel.Children.Add(new Label()
            {
                Content = content,
                FontFamily = new FontFamily("Sitka Text"),
                FontSize = 16
            });
        }

        private void StackPanelAddRightChild(string content)
        {
            RightInnerStackPanel.Children.Add(new Label()
            {
                Content = content,
                FontFamily = new FontFamily("Sitka Text"),
                FontSize = 16
            });
        }

        private void SetUpGridContextMenuItems()
        {
            // Reset context menu 
            WVA_OrdersContextMenu.Items.Clear();

            if (SelectedRow < 0)
                return;
           
            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].DeletedFlag != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].DeletedFlag.ToLower() == "y")
            {
                MenuItem item = new MenuItem() { Header = "-- DELETED ORDER --" };
                item.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(item);
            }

            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityBackordered > 0)
            {
                MenuItem item = new MenuItem() { Header = $"Quantity backordered: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityBackordered}" };
                item.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(item);
            }

            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityCancelled > 0)
            {
                MenuItem item = new MenuItem() { Header = $"Quantity cancelled: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityCancelled}" };
                item.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(item);
            }

            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityShipped > 0)
            {
                MenuItem numShippedItem = new MenuItem() { Header = $"Quantity shipped: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityShipped}" };
                numShippedItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(numShippedItem);
            }

            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].Status != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].Status.Trim() != "")
            {
                MenuItem statusItem = new MenuItem() { Header = $"Status: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].Status}" };
                statusItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(statusItem);
            }

            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ItemStatus != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ItemStatus.Trim() != "")
            {
                MenuItem itemStatusItem = new MenuItem() { Header = $"Item Status: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ItemStatus}" };
                itemStatusItem.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(itemStatusItem);
            }

            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingDate != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingDate.Trim() != "")
            {
                MenuItem item = new MenuItem() { Header = $"Shipping Date: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingDate}" };
                item.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(item);
            }

            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingCarrier != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingCarrier.Trim() != "")
            {
                MenuItem item = new MenuItem() { Header = $"Shipping Carrier: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingCarrier}" };
                item.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(item);
            }

            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingUrl != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingUrl.Trim() != "")
            {
                MenuItem item = new MenuItem() { Header = $"{ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingUrl}" };
                item.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(item);
            }

            if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingNumber != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingNumber.Trim() != "")
            {
                MenuItem item = new MenuItem() { Header = $"Tracking Number: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingNumber}" };
                item.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(item);
            }

        }

        private void WVA_OrdersContextMenu_Click(object sender, RoutedEventArgs e)
        {
           


        }

        private void ReviewOrderDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            SelectedRow = ReviewOrderDataGrid.Items.IndexOf(ReviewOrderDataGrid.CurrentItem);
            SetUpGridContextMenuItems();
        }
    }
}
