﻿#pragma checksum "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "75C0DBE2914B407B641A054D338487952868B2C8"
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
using WVA_Compulink_Integration.Views.Login;


namespace WVA_Compulink_Integration.Views.Login {
    
    
    /// <summary>
    /// ForgotPasswordWindow
    /// </summary>
    public partial class ForgotPasswordWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 158 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseButton;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label UserNameLabel;
        
        #line default
        #line hidden
        
        
        #line 189 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UserNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 199 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SendEmailButton;
        
        #line default
        #line hidden
        
        
        #line 209 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MessageLabel;
        
        #line default
        #line hidden
        
        
        #line 218 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CodeLabel;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CodeTextBox;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitCodeButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WVA_Compulink_Integration;component/views/login/forgotpasswordwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
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
            
            #line 15 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
            ((WVA_Compulink_Integration.Views.Login.ForgotPasswordWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
            ((WVA_Compulink_Integration.Views.Login.ForgotPasswordWindow)(target)).Initialized += new System.EventHandler(this.Window_Initialized);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 157 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
            this.CloseButton.Click += new System.Windows.RoutedEventHandler(this.CloseButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.UserNameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.UserNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 197 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
            this.UserNameTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.EmailTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SendEmailButton = ((System.Windows.Controls.Button)(target));
            
            #line 207 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
            this.SendEmailButton.Click += new System.Windows.RoutedEventHandler(this.SendEmailButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MessageLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.CodeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.CodeTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 233 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
            this.CodeTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.CodeTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SubmitCodeButton = ((System.Windows.Controls.Button)(target));
            
            #line 243 "..\..\..\..\Views\Login\ForgotPasswordWindow.xaml"
            this.SubmitCodeButton.Click += new System.Windows.RoutedEventHandler(this.SubmitCodeButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

