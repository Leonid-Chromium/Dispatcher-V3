﻿#pragma checksum "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0463EA35EA478152F9C42AFA31764EEF91C4319C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Dispatcher.UCs.SUCs;
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


namespace Dispatcher.UCs.SUCs {
    
    
    /// <summary>
    /// RoleSUC
    /// </summary>
    public partial class RoleSUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RoleCB;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RoleIdTB;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RoleNameTB;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RolePasswordTB;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RoleHashTB;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddButton;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditButton;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Dispatcher;V1.0.0.0;component/ucs/sucs/rolesuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
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
            
            #line 9 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
            ((Dispatcher.UCs.SUCs.RoleSUC)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RoleCB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 39 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
            this.RoleCB.DropDownClosed += new System.EventHandler(this.RoleCB_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RoleIdTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.RoleNameTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.RolePasswordTB = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
            this.RolePasswordTB.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.RolePasswordTB_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RoleHashTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.AddButton = ((System.Windows.Controls.Button)(target));
            
            #line 90 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
            this.AddButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.EditButton = ((System.Windows.Controls.Button)(target));
            
            #line 95 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
            this.EditButton.Click += new System.Windows.RoutedEventHandler(this.EditButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.DeleteButton = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\..\..\..\UCs\SUCs\RoleSUC.xaml"
            this.DeleteButton.Click += new System.Windows.RoutedEventHandler(this.DeleteButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

