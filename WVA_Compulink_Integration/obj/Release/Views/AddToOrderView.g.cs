﻿#pragma checksum "..\..\..\Views\AddToOrderView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E212FE4E4A1A948BAB001A10F54766A688197E5C"
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
using WVA_Compulink_Integration.Views;


namespace WVA_Compulink_Integration.Views {
    
    
    /// <summary>
    /// AddToOrderView
    /// </summary>
    public partial class AddToOrderView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 141 "..\..\..\Views\AddToOrderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle HeadTopRect;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\..\Views\AddToOrderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PatienNameLabel;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\Views\AddToOrderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle HeadBottomRect;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\Views\AddToOrderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PrescriptionDataGrid;
        
        #line default
        #line hidden
        
        
        #line 252 "..\..\..\Views\AddToOrderView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddToOrderButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WVA_Compulink_Integration;component/views/addtoorderview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AddToOrderView.xaml"
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
            this.HeadTopRect = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 2:
            this.PatienNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.HeadBottomRect = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 4:
            this.PrescriptionDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 179 "..\..\..\Views\AddToOrderView.xaml"
            this.PrescriptionDataGrid.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.PrescriptionDataGrid_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.AddToOrderButton = ((System.Windows.Controls.Button)(target));
            
            #line 255 "..\..\..\Views\AddToOrderView.xaml"
            this.AddToOrderButton.Click += new System.Windows.RoutedEventHandler(this.AddToOrderButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
