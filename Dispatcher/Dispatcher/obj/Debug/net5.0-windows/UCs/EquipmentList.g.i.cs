﻿#pragma checksum "..\..\..\..\UCs\EquipmentList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "89C84818046AE4441A506155CA809F67016C0368"
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
    /// EquipmentList
    /// </summary>
    public partial class EquipmentList : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\..\UCs\EquipmentList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SortName;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\UCs\EquipmentList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FilterDistrict;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\UCs\EquipmentList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FilterStatus;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\UCs\EquipmentList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTB;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\UCs\EquipmentList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RefreshButton;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\UCs\EquipmentList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ListSV;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\UCs\EquipmentList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ListSP;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.3.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Dispatcher;component/ucs/equipmentlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UCs\EquipmentList.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.3.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\UCs\EquipmentList.xaml"
            ((Dispatcher.UCs.EquipmentList)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SortName = ((System.Windows.Controls.ComboBox)(target));
            
            #line 42 "..\..\..\..\UCs\EquipmentList.xaml"
            this.SortName.DropDownClosed += new System.EventHandler(this.SortName_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FilterDistrict = ((System.Windows.Controls.ComboBox)(target));
            
            #line 53 "..\..\..\..\UCs\EquipmentList.xaml"
            this.FilterDistrict.DropDownClosed += new System.EventHandler(this.FilterDistrict_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 4:
            this.FilterStatus = ((System.Windows.Controls.ComboBox)(target));
            
            #line 62 "..\..\..\..\UCs\EquipmentList.xaml"
            this.FilterStatus.DropDownClosed += new System.EventHandler(this.FilterStatus_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SearchTB = ((System.Windows.Controls.TextBox)(target));
            
            #line 69 "..\..\..\..\UCs\EquipmentList.xaml"
            this.SearchTB.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchTB_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RefreshButton = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\..\UCs\EquipmentList.xaml"
            this.RefreshButton.Click += new System.Windows.RoutedEventHandler(this.RefreshButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ListSV = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 8:
            this.ListSP = ((System.Windows.Controls.StackPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

