﻿#pragma checksum "..\..\..\..\UCs\EquipmentCard.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BB62EA17326A5605DFD380681A29B175A2D9D07B"
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
    /// EquipmentCard
    /// </summary>
    public partial class EquipmentCard : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\UCs\EquipmentCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label EquipmentNameL;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\UCs\EquipmentCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label EquipmentDistrictL;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\UCs\EquipmentCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label StatusL;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\UCs\EquipmentCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle StatusLED;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\UCs\EquipmentCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button InfoButton;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\UCs\EquipmentCard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Dispatcher;component/ucs/equipmentcard.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UCs\EquipmentCard.xaml"
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
            
            #line 9 "..\..\..\..\UCs\EquipmentCard.xaml"
            ((Dispatcher.UCs.EquipmentCard)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.EquipmentNameL = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.EquipmentDistrictL = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.StatusL = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.StatusLED = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 6:
            this.InfoButton = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\..\UCs\EquipmentCard.xaml"
            this.InfoButton.Click += new System.Windows.RoutedEventHandler(this.InfoButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.EditButton = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\..\UCs\EquipmentCard.xaml"
            this.EditButton.Click += new System.Windows.RoutedEventHandler(this.EditButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

