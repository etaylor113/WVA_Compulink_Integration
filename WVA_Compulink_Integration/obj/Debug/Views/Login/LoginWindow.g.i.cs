﻿#pragma checksum "..\..\..\..\Views\Login\LoginWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6449B9B9F7CAF34C1A66FAE5CE0E4C9D966DE8E8"
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
    /// LoginWindow
    /// </summary>
    public partial class LoginWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 163 "..\..\..\..\Views\Login\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UsernameTextBox;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\..\..\Views\Login\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\..\Views\Login\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoginButton;
        
        #line default
        #line hidden
        
        
        #line 211 "..\..\..\..\Views\Login\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NotifyLabel;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\..\..\Views\Login\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CreateAccountLink;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\..\..\Views\Login\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ForgotPasswordLink;
        
        #line default
        #line hidden
        
        
        #line 250 "..\..\..\..\Views\Login\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WVA_Compulink_Integration;component/views/login/loginwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Login\LoginWindow.xaml"
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
            this.UsernameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 173 "..\..\..\..\Views\Login\LoginWindow.xaml"
            this.UsernameTextBox.KeyUp += new System.Windows.Input.KeyEventHandler(this.UsernameTextBox_KeyUp);
            
            #line default
            #line hidden
            
            #line 174 "..\..\..\..\Views\Login\LoginWindow.xaml"
            this.UsernameTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.UsernameTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PasswordTextBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 194 "..\..\..\..\Views\Login\LoginWindow.xaml"
            this.PasswordTextBox.KeyUp += new System.Windows.Input.KeyEventHandler(this.PasswordTextBox_KeyUp);
            
            #line default
            #line hidden
            
            #line 195 "..\..\..\..\Views\Login\LoginWindow.xaml"
            this.PasswordTextBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.PasswordTextBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.LoginButton = ((System.Windows.Controls.Button)(target));
            
            #line 206 "..\..\..\..\Views\Login\LoginWindow.xaml"
            this.LoginButton.Click += new System.Windows.RoutedEventHandler(this.LoginButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.NotifyLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.CreateAccountLink = ((System.Windows.Controls.TextBlock)(target));
            
            #line 226 "..\..\..\..\Views\Login\LoginWindow.xaml"
            this.CreateAccountLink.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.CreateAccountLink_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ForgotPasswordLink = ((System.Windows.Controls.TextBlock)(target));
            
            #line 240 "..\..\..\..\Views\Login\LoginWindow.xaml"
            this.ForgotPasswordLink.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.ForgotPasswordLink_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 249 "..\..\..\..\Views\Login\LoginWindow.xaml"
            this.CloseButton.Click += new System.Windows.RoutedEventHandler(this.CloseButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

