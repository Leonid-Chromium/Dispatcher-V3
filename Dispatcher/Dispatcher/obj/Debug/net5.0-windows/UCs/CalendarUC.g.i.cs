﻿#pragma checksum "..\..\..\..\UCs\CalendarUC.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3F325FA8E649A0BCBCEFAE1C3679A620D6EFC1D0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Dispatcher.UCs;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Dispatcher.UCs {
    
    
    /// <summary>
    /// CalendarUC
    /// </summary>
    public partial class CalendarUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\UCs\CalendarUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar Calendar;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\UCs\CalendarUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker FirstDatePicker;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\UCs\CalendarUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker SecondDatePicker;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\UCs\CalendarUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Default;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Dispatcher;V1.0.0.0;component/ucs/calendaruc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UCs\CalendarUC.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Calendar = ((System.Windows.Controls.Calendar)(target));
            return;
            case 2:
            this.FirstDatePicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 32 "..\..\..\..\UCs\CalendarUC.xaml"
            this.FirstDatePicker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.FirstDatePicker_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SecondDatePicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 36 "..\..\..\..\UCs\CalendarUC.xaml"
            this.SecondDatePicker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.SecondDatePicker_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Default = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\UCs\CalendarUC.xaml"
            this.Default.Click += new System.Windows.RoutedEventHandler(this.Default_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
