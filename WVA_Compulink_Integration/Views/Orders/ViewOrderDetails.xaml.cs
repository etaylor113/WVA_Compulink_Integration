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
using WVA_Compulink_Integration.Error;
using WVA_Compulink_Integration.Models.Order.Out;
using WVA_Compulink_Integration.Models.Prescription;
using WVA_Compulink_Integration.Utility.Actions;
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
            try
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
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
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
            try
            {
                // Reset context menu 
                WVA_OrdersContextMenu.Items.Clear();

                if (SelectedRow < 0)
                    return;

                // <START> FOR TESTING ONLY!!!
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].DeletedFlag = "y";
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityBackordered = 1;
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityCancelled = 1;
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityShipped = 4;
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].Status = "Shipped";
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ItemStatus = "AS466SDF654";
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingDate = "04/03/19";
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingCarrier = "UPS";
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingUrl = "https://Google.com";
                ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingNumber = "654687463546";
                // <END>

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].DeletedFlag != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].DeletedFlag.ToLower() == "y")
                    AddItemToGridContextMenu("-- DELETED ORDER! --");

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityBackordered > 0)
                    AddItemToGridContextMenu($"Quantity backordered: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityBackordered}");

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityCancelled > 0)
                    AddItemToGridContextMenu($"Quantity cancelled: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityCancelled}");

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityShipped > 0)
                    AddItemToGridContextMenu($"Quantity shipped: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].QuantityShipped}");

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].Status != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].Status.Trim() != "")
                    AddItemToGridContextMenu($"Status: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].Status}");

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ItemStatus != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ItemStatus.Trim() != "")
                    AddItemToGridContextMenu($"Item Status: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ItemStatus}");

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingDate != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingDate.Trim() != "")
                    AddItemToGridContextMenu($"Shipping Date: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingDate}");

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingCarrier != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingCarrier.Trim() != "")
                    AddItemToGridContextMenu($"Shipping Carrier: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].ShippingCarrier}");

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingUrl != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingUrl.Trim() != "")
                    AddItemToGridContextMenu($"{ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingUrl}");

                if (ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingNumber != null && ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingNumber.Trim() != "")
                    AddItemToGridContextMenu($"Tracking Number: {ViewOrderDetailsViewModel.SelectedOrder.Items[SelectedRow].TrackingNumber}");
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        private void AddItemToGridContextMenu(string headerContent)
        {
            try
            {
                MenuItem item = new MenuItem() { Header = headerContent };
                item.Click += new RoutedEventHandler(WVA_OrdersContextMenu_Click);
                WVA_OrdersContextMenu.Items.Add(item);
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        private void WVA_OrdersContextMenu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem menuItem = sender as MenuItem;
                string selectedItem = menuItem.Header.ToString();

                if (selectedItem.Contains("https"))
                {
                    Process.Start(selectedItem);
                }
            }
            catch (Exception ex)
            {
                AppError.ReportOrWrite(ex);
            }
        }

        private void ReviewOrderDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            SelectedRow = ReviewOrderDataGrid.Items.IndexOf(ReviewOrderDataGrid.CurrentItem);
            SetUpGridContextMenuItems();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string location = e.Source.ToString() + "UserControl_Loaded()";
            string actionMessage = $"<Order.ID={ViewOrderDetailsViewModel.SelectedOrder.ID}>, <Order.Name={ViewOrderDetailsViewModel.SelectedOrder.OrderName}>, <Order.WvaOrderID={ViewOrderDetailsViewModel.SelectedOrder.WvaStoreID}>";
            ActionLogger.Log(location, actionMessage);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            string location = e.Source.ToString() + "UserControl_Unloaded()";
            string actionMessage = $"<Order.ID={ViewOrderDetailsViewModel.SelectedOrder.ID}>, <Order.Name={ViewOrderDetailsViewModel.SelectedOrder.OrderName}>, <Order.WvaOrderID={ViewOrderDetailsViewModel.SelectedOrder.WvaStoreID}>";
            ActionLogger.Log(location, actionMessage);
        }
    }
}
