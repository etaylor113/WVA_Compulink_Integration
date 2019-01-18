﻿#pragma checksum "..\..\..\..\Views\Orders\WVAOrders.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AA774AB060553904D80986C23E82EA7B850EE428"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WVA_Compulink_Integration.Views.Orders;


namespace WVA_Compulink_Integration.Views.Orders {
    
    
    /// <summary>
    /// WVAOrders
    /// </summary>
    public partial class WVAOrders : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 150 "..\..\..\..\Views\Orders\WVAOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\..\Views\Orders\WVAOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SearchFilterComboBox;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\..\..\Views\Orders\WVAOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RefreshButton;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\..\Views\Orders\WVAOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image RefreshImage;
        
        #line default
        #line hidden
        
        
        #line 193 "..\..\..\..\Views\Orders\WVAOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid OrdersDataGrid;
        
        #line default
        #line hidden
        
        
        #line 263 "..\..\..\..\Views\Orders\WVAOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitButton;
        
        #line default
        #line hidden
        
        
        #line 270 "..\..\..\..\Views\Orders\WVAOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditOrderButton;
        
        #line default
        #line hidden
        
        
        #line 277 "..\..\..\..\Views\Orders\WVAOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteOrderButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WVA_Compulink_Integration;component/views/orders/wvaorders.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Orders\WVAOrders.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 158 "..\..\..\..\Views\Orders\WVAOrders.xaml"
            this.SearchTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SearchFilterComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.RefreshButton = ((System.Windows.Controls.Button)(target));
            
            #line 179 "..\..\..\..\Views\Orders\WVAOrders.xaml"
            this.RefreshButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.RefreshButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 180 "..\..\..\..\Views\Orders\WVAOrders.xaml"
            this.RefreshButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.RefreshButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 181 "..\..\..\..\Views\Orders\WVAOrders.xaml"
            this.RefreshButton.Click += new System.Windows.RoutedEventHandler(this.RefreshButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.RefreshImage = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.OrdersDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.SubmitButton = ((System.Windows.Controls.Button)(target));
            
            #line 268 "..\..\..\..\Views\Orders\WVAOrders.xaml"
            this.SubmitButton.Click += new System.Windows.RoutedEventHandler(this.SubmitButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.EditOrderButton = ((System.Windows.Controls.Button)(target));
            
            #line 275 "..\..\..\..\Views\Orders\WVAOrders.xaml"
            this.EditOrderButton.Click += new System.Windows.RoutedEventHandler(this.EditOrderButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DeleteOrderButton = ((System.Windows.Controls.Button)(target));
            
            #line 282 "..\..\..\..\Views\Orders\WVAOrders.xaml"
            this.DeleteOrderButton.Click += new System.Windows.RoutedEventHandler(this.DeleteOrderButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

