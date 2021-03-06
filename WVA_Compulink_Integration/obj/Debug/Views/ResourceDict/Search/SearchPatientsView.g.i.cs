﻿#pragma checksum "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7320D8DCD87C18145C040188517D1AA0B632841E"
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
    /// SearchPatientsView
    /// </summary>
    public partial class SearchPatientsView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 141 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SearchTypeLabel;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PatientFilterComboBox;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label SearchLabel;
        
        #line default
        #line hidden
        
        
        #line 198 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox QuickSearchCheckBox;
        
        #line default
        #line hidden
        
        
        #line 220 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 232 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RefreshButton;
        
        #line default
        #line hidden
        
        
        #line 244 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle TopDividerLine;
        
        #line default
        #line hidden
        
        
        #line 252 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PatientDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/WVA_Compulink_Integration;component/views/resourcedict/search/searchpatientsview" +
                    ".xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
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
            this.SearchTypeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.PatientFilterComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.SearchLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 205 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
            this.SearchTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.QuickSearchCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 229 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.SearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.RefreshButton = ((System.Windows.Controls.Button)(target));
            
            #line 241 "..\..\..\..\..\Views\ResourceDict\Search\SearchPatientsView.xaml"
            this.RefreshButton.Click += new System.Windows.RoutedEventHandler(this.RefreshButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TopDividerLine = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 9:
            this.PatientDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

