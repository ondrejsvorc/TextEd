﻿#pragma checksum "..\..\FindReplace.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4CE222284737DC8F42890A9F082A46E4EAF1ACF25A58AD26170157AC61CCE416"
//------------------------------------------------------------------------------
// <auto-generated>
//     Tento kód byl generován nástrojem.
//     Verze modulu runtime:4.0.30319.42000
//
//     Změny tohoto souboru mohou způsobit nesprávné chování a budou ztraceny,
//     dojde-li k novému generování kódu.
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
using TextEd;


namespace TextEd {
    
    
    /// <summary>
    /// FindReplace
    /// </summary>
    public partial class FindReplace : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\FindReplace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TextEd.FindReplace findReplaceWindow;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\FindReplace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem find;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\FindReplace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem replace;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\FindReplace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbFind;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\FindReplace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbReplace;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\FindReplace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoxFind;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\FindReplace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoxReplaceWith;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\FindReplace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConfirm;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\FindReplace.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxCaseSensitive;
        
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
            System.Uri resourceLocater = new System.Uri("/TextEd;component/findreplace.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FindReplace.xaml"
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
            this.findReplaceWindow = ((TextEd.FindReplace)(target));
            
            #line 8 "..\..\FindReplace.xaml"
            this.findReplaceWindow.Closing += new System.ComponentModel.CancelEventHandler(this.WindowIsClosing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.find = ((System.Windows.Controls.MenuItem)(target));
            
            #line 11 "..\..\FindReplace.xaml"
            this.find.MouseEnter += new System.Windows.Input.MouseEventHandler(this.MouseEnterToBold);
            
            #line default
            #line hidden
            
            #line 11 "..\..\FindReplace.xaml"
            this.find.MouseLeave += new System.Windows.Input.MouseEventHandler(this.MouseLeaveToNormal);
            
            #line default
            #line hidden
            
            #line 11 "..\..\FindReplace.xaml"
            this.find.Click += new System.Windows.RoutedEventHandler(this.MenuItemClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.replace = ((System.Windows.Controls.MenuItem)(target));
            
            #line 12 "..\..\FindReplace.xaml"
            this.replace.MouseEnter += new System.Windows.Input.MouseEventHandler(this.MouseEnterToBold);
            
            #line default
            #line hidden
            
            #line 12 "..\..\FindReplace.xaml"
            this.replace.MouseLeave += new System.Windows.Input.MouseEventHandler(this.MouseLeaveToNormal);
            
            #line default
            #line hidden
            
            #line 12 "..\..\FindReplace.xaml"
            this.replace.Click += new System.Windows.RoutedEventHandler(this.MenuItemClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lbFind = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lbReplace = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.txtBoxFind = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\FindReplace.xaml"
            this.txtBoxFind.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.Highlight);
            
            #line default
            #line hidden
            return;
            case 7:
            this.txtBoxReplaceWith = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btnConfirm = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\FindReplace.xaml"
            this.btnConfirm.Click += new System.Windows.RoutedEventHandler(this.Replace);
            
            #line default
            #line hidden
            return;
            case 9:
            this.checkBoxCaseSensitive = ((System.Windows.Controls.CheckBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
