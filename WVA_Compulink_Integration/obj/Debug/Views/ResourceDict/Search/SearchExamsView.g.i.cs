﻿#pragma checksum "..\..\..\..\..\Views\ResourceDict\Search\SearchExamsView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D0AF3CD6D49CB05483FCFE81D5704820937E6FB2"
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


namespace WVA_Compulink_Integration.Views.Search {
    
    
    /// <summary>
    /// SearchExamsView
    /// </summary>
    public partial class SearchExamsView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 144 "..\..\..\..\..\Views\ResourceDict\Search\SearchExamsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle TopDividerLine;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\..\..\Views\ResourceDict\Search\SearchExamsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RefreshDataBtn;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\..\..\Views\ResourceDict\Search\SearchExamsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrevDayBtn;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\..\..\Views\ResourceDict\Search\SearchExamsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextDayBtn;
        
        #line default
        #line hidden
        
        
        #line 192 "..\..\..\..\..\Views\ResourceDict\Search\SearchExamsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox LocationsComboBox;
        
        #line default
        #line hidden
        
        
        #line 204 "..\..\..\..\..\Views\ResourceDict\Search\SearchExamsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ExamDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/WVA_Compulink_Integration;component/views/resourcedict/search/searchexamsview.xa" +
                    "ml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\ResourceDict\Search\SearchExamsView.xaml"
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
            this.TopDividerLine = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 2:
            this.RefreshDataBtn = ((System.Windows.Controls.Button)(target));
            
            #line 159 "..\..\..\..\..\Views\ResourceDict\Search\SearchExamsView.xaml"
            this.RefreshDataBtn.Click += new System.Windows.RoutedEventHandler(this.RefreshDataBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PrevDayBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.NextDayBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.LocationsComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.ExamDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

