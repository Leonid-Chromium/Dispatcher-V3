﻿#pragma checksum "..\..\..\..\UCs\AddTKKK.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "17E6CB9BB7E2938D95CFE709EEA62FE4BAAB6865"
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
    /// AddTKKK
    /// </summary>
    public partial class AddTKKK : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\UCs\AddTKKK.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameTB;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\UCs\AddTKKK.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CodeTB;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\UCs\AddTKKK.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LarsTB;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\UCs\AddTKKK.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PathTB;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\UCs\AddTKKK.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox DistrictCB;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\UCs\AddTKKK.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox OperationTypeCB;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\..\UCs\AddTKKK.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ClearButton;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\UCs\AddTKKK.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Dispatcher;component/ucs/addtkkk.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UCs\AddTKKK.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\UCs\AddTKKK.xaml"
            ((Dispatcher.UCs.AddTKKK)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NameTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.CodeTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.LarsTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.PathTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.DistrictCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 78 "..\..\..\..\UCs\AddTKKK.xaml"
            this.DistrictCB.DropDownClosed += new System.EventHandler(this.DistrictCB_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 7:
            this.OperationTypeCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 91 "..\..\..\..\UCs\AddTKKK.xaml"
            this.OperationTypeCB.DropDownClosed += new System.EventHandler(this.OperationTypeCB_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ClearButton = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\..\UCs\AddTKKK.xaml"
            this.ClearButton.Click += new System.Windows.RoutedEventHandler(this.ClearButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.AddButton = ((System.Windows.Controls.Button)(target));
            
            #line 99 "..\..\..\..\UCs\AddTKKK.xaml"
            this.AddButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

