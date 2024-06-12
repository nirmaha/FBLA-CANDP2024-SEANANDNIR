﻿#pragma checksum "..\..\..\..\..\MVVM\View\Pages\Dashboard.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8159B2883E18AD8DA02EF4DC71F97AD246C8852B0C2DA66D434AD69058071BE1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EduPartners.MVVM.View.Pages;
using LiveCharts.Wpf;
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
    /// Dashboard
    /// </summary>
    public partial class Dashboard : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\..\..\MVVM\View\Pages\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.CartesianChart lineNumofPartners;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\MVVM\View\Pages\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.PieChart pieIndustry;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\..\MVVM\View\Pages\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox legendListBox;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\..\MVVM\View\Pages\Dashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal LiveCharts.Wpf.CartesianChart barIndustrySavings;
        
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
            System.Uri resourceLocater = new System.Uri("/EduPartners;component/mvvm/view/pages/dashboard.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\MVVM\View\Pages\Dashboard.xaml"
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
            this.lineNumofPartners = ((LiveCharts.Wpf.CartesianChart)(target));
            return;
            case 2:
            this.pieIndustry = ((LiveCharts.Wpf.PieChart)(target));
            
            #line 62 "..\..\..\..\..\MVVM\View\Pages\Dashboard.xaml"
            this.pieIndustry.DataClick += new LiveCharts.Events.DataClickHandler(this.PieChart_DataClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.legendListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 4:
            this.barIndustrySavings = ((LiveCharts.Wpf.CartesianChart)(target));
            
            #line 83 "..\..\..\..\..\MVVM\View\Pages\Dashboard.xaml"
            this.barIndustrySavings.DataClick += new LiveCharts.Events.DataClickHandler(this.BarGraph_DataClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

