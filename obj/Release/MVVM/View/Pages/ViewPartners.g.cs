﻿#pragma checksum "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "588C3BC12187A1AD531601F3753899218F1BE8D476C1E1C6EFEE6D00142B3FCD"
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
using EduPartners.MVVM.View.Pages;
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


namespace EduPartners.MVVM.View.Pages {
    
    
    /// <summary>
    /// ViewPartners
    /// </summary>
    public partial class ViewPartners : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 35 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel spFilterButtons;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSearchFilter;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSerachBox;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lSearchLabel;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl icViewParnter;
        
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
            System.Uri resourceLocater = new System.Uri("/EduPartners;component/mvvm/view/pages/viewpartners.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
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
            
            #line 20 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MainBorder_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.spFilterButtons = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            
            #line 38 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FilterButton_Clicked);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 43 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FilterButton_Clicked);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 48 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FilterButton_Clicked);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 53 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Click += new System.Windows.RoutedEventHandler(this.FilterButton_Clicked);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 59 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveAll_Clicked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cbSearchFilter = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.tbSerachBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 129 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            this.tbSerachBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbSearchBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lSearchLabel = ((System.Windows.Controls.Label)(target));
            
            #line 130 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            this.lSearchLabel.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lSearchLabel_MouseDown);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 133 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Search_MouseDown);
            
            #line default
            #line hidden
            return;
            case 12:
            this.icViewParnter = ((System.Windows.Controls.ItemsControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 13:
            
            #line 170 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Card_MouseDown);
            
            #line default
            #line hidden
            break;
            case 14:
            
            #line 192 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Edit_MouseDown);
            
            #line default
            #line hidden
            break;
            case 15:
            
            #line 197 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Print_MouseDown);
            
            #line default
            #line hidden
            break;
            case 16:
            
            #line 202 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Delete_MouseDown);
            
            #line default
            #line hidden
            break;
            case 17:
            
            #line 241 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).RequestNavigate += new System.Windows.Navigation.RequestNavigateEventHandler(this.Hyperlink_RequestNavigate);
            
            #line default
            #line hidden
            break;
            case 18:
            
            #line 264 "..\..\..\..\..\MVVM\View\Pages\ViewPartners.xaml"
            ((System.Windows.Documents.Hyperlink)(target)).RequestNavigate += new System.Windows.Navigation.RequestNavigateEventHandler(this.Hyperlink_RequestNavigate);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

