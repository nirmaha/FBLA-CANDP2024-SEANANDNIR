﻿#pragma checksum "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C1861F200CB59F116EE9F0EB0A1E687E55D956AD57A168DDA782C5157222D97D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EduPartners.MVVM.View.Controls;
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


namespace EduPartners.MVVM.View.Controls {
    
    
    /// <summary>
    /// HomePage
    /// </summary>
    public partial class HomePage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal EduPartners.MVVM.View.Controls.HomePage Home;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageBrush ibBackground;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btCreateSchool;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btLogin;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btSignUp;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lBackgroundText;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border firstPanelCircle;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border secondPanelCircle;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border thirdPanelCircle;
        
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
            System.Uri resourceLocater = new System.Uri("/EduPartners;component/mvvm/view/controls/homepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
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
            this.Home = ((EduPartners.MVVM.View.Controls.HomePage)(target));
            return;
            case 2:
            this.ibBackground = ((System.Windows.Media.ImageBrush)(target));
            return;
            case 3:
            this.btCreateSchool = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
            this.btCreateSchool.Click += new System.Windows.RoutedEventHandler(this.CreateSchool_Clicked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btLogin = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
            this.btLogin.Click += new System.Windows.RoutedEventHandler(this.Login_Clicked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btSignUp = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
            this.btSignUp.Click += new System.Windows.RoutedEventHandler(this.SignUp_Clicked);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 59 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.bHelp_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 66 "..\..\..\..\..\MVVM\View\Controls\HomePage.xaml"
            ((System.Windows.Controls.Grid)(target)).PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Background_Clicked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lBackgroundText = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.firstPanelCircle = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.secondPanelCircle = ((System.Windows.Controls.Border)(target));
            return;
            case 11:
            this.thirdPanelCircle = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

