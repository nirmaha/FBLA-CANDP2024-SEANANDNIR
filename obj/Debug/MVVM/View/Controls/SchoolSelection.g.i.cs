﻿#pragma checksum "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8AF8566B230A37B7F945FA68E5471623635F79C7AFAB9C2D0023C547FB11A8CB"
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
    /// SchoolSelection
    /// </summary>
    public partial class SchoolSelection : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal EduPartners.MVVM.View.Controls.SchoolSelection SelectSchool;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SecondColumnGrid;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border bSchoolSelectionBorder;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lErrorMessage;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSchool;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSchoolId;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lSchoolId;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCreateAccount;
        
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
            System.Uri resourceLocater = new System.Uri("/EduPartners;component/mvvm/view/controls/schoolselection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
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
            this.SelectSchool = ((EduPartners.MVVM.View.Controls.SchoolSelection)(target));
            return;
            case 2:
            
            #line 21 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnBack_Clicked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SecondColumnGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.bSchoolSelectionBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            this.lErrorMessage = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.cbSchool = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.tbSchoolId = ((System.Windows.Controls.TextBox)(target));
            
            #line 47 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
            this.tbSchoolId.GotFocus += new System.Windows.RoutedEventHandler(this.tbSchoolId_GotFocus);
            
            #line default
            #line hidden
            
            #line 47 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
            this.tbSchoolId.LostFocus += new System.Windows.RoutedEventHandler(this.tbSchoolId_LostFocus);
            
            #line default
            #line hidden
            
            #line 47 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
            this.tbSchoolId.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbSchoolId_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lSchoolId = ((System.Windows.Controls.Label)(target));
            
            #line 48 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
            this.lSchoolId.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lSchoolId_Clicked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCreateAccount = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
            this.btnCreateAccount.Click += new System.Windows.RoutedEventHandler(this.btnCreateAccount_Clicked);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 68 "..\..\..\..\..\MVVM\View\Controls\SchoolSelection.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.SignUpRedirect_MouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

