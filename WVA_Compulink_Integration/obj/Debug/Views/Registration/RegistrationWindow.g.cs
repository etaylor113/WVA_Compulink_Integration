﻿#pragma checksum "..\..\..\..\Views\Registration\RegistrationWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A5B73E8C7AB70D1FEEA0DA032A7C5008D5A22FBA"
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
using WVA_Compulink_Integration.Views.Registration;


namespace WVA_Compulink_Integration.Views.Registration {
    
    
    /// <summary>
    /// RegistrationWindow
    /// </summary>
    public partial class RegistrationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 171 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox EmailTextBox;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox UserNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 211 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 229 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox ConfirmPasswordTextBox;
        
        #line default
        #line hidden
        
        
        #line 240 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitButton;
        
        #line default
        #line hidden
        
        
        #line 252 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock BackToLoginLink;
        
        #line default
        #line hidden
        
        
        #line 266 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NotifyLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/WVA_Compulink_Integration;component/views/registration/registrationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
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
            this.EmailTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 181 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
            this.EmailTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.EmailTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.UserNameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 201 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
            this.UserNameTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.UserNameTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PasswordTextBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 219 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
            this.PasswordTextBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.PasswordTextBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ConfirmPasswordTextBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 237 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
            this.ConfirmPasswordTextBox.PasswordChanged += new System.Windows.RoutedEventHandler(this.ConfirmPasswordTextBox_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SubmitButton = ((System.Windows.Controls.Button)(target));
            
            #line 249 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
            this.SubmitButton.Click += new System.Windows.RoutedEventHandler(this.SubmitButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BackToLoginLink = ((System.Windows.Controls.TextBlock)(target));
            
            #line 259 "..\..\..\..\Views\Registration\RegistrationWindow.xaml"
            this.BackToLoginLink.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.BackToLoginLink_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NotifyLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

