﻿#pragma checksum "..\..\..\..\MVVM\View\LoginWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6F152F4C03FE616F2E87EE287952142BC33503B4D7750BE9F6BAEAEB983269F9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EduPartners.MVVM.View;
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


namespace EduPartners.MVVM.View {
    
    
    /// <summary>
    /// LoginWindow
    /// </summary>
    public partial class LoginWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LoginMainGrid;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SecondColumnGrid;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border bLoginBorder;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbEmail;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lEmail;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox pbPassword;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lPassword;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label tbForgotPassword;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbRememberMe;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\MVVM\View\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLogin;
        
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
            System.Uri resourceLocater = new System.Uri("/EduPartners;component/mvvm/view/loginwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MVVM\View\LoginWindow.xaml"
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
            this.LoginMainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 26 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnBack_Clicked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SecondColumnGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.bLoginBorder = ((System.Windows.Controls.Border)(target));
            
            #line 36 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.bLoginBorder.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LoginBorder_MouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbEmail = ((System.Windows.Controls.TextBox)(target));
            
            #line 48 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.tbEmail.GotFocus += new System.Windows.RoutedEventHandler(this.tbEmail_GotFocus);
            
            #line default
            #line hidden
            
            #line 48 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.tbEmail.LostFocus += new System.Windows.RoutedEventHandler(this.tbEmail_LostFocus);
            
            #line default
            #line hidden
            
            #line 48 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.tbEmail.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbEmail_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lEmail = ((System.Windows.Controls.Label)(target));
            
            #line 49 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.lEmail.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lEmail_Clicked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.pbPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 51 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.pbPassword.GotFocus += new System.Windows.RoutedEventHandler(this.pbPassword_GotFocus);
            
            #line default
            #line hidden
            
            #line 51 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.pbPassword.LostFocus += new System.Windows.RoutedEventHandler(this.pbPassword_LostFocus);
            
            #line default
            #line hidden
            
            #line 51 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.pbPassword.PasswordChanged += new System.Windows.RoutedEventHandler(this.pbPassword_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lPassword = ((System.Windows.Controls.Label)(target));
            
            #line 52 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.lPassword.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lPassword_Clicked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.tbForgotPassword = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.cbRememberMe = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 11:
            this.btnLogin = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            this.btnLogin.Click += new System.Windows.RoutedEventHandler(this.btnLogin_Clicked);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 79 "..\..\..\..\MVVM\View\LoginWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.SignUpRedirect_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

