﻿#pragma checksum "..\..\Coise_ticket.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E577D07C96D3980885E8780697C884897C47213D56BB928F3146687A372C5E74"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using priviazka;


namespace priviazka {
    
    
    /// <summary>
    /// Coise_ticket
    /// </summary>
    public partial class Coise_ticket : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\Coise_ticket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Adminlist;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Coise_ticket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbEventname;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Coise_ticket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddEvent;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Coise_ticket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Alllist;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Coise_ticket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbEventList;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Coise_ticket.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnChoisen;
        
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
            System.Uri resourceLocater = new System.Uri("/priviazka;component/coise_ticket.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Coise_ticket.xaml"
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
            this.Adminlist = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.tbEventname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.btnAddEvent = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\Coise_ticket.xaml"
            this.btnAddEvent.Click += new System.Windows.RoutedEventHandler(this.btnAddEvent_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Alllist = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.cbEventList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.btnChoisen = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\Coise_ticket.xaml"
            this.btnChoisen.Click += new System.Windows.RoutedEventHandler(this.btnChoisen_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

