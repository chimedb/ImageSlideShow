﻿#pragma checksum "..\..\Image.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8B90C7E98701A51402E8BEF04F3F7334895CA2C1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using WPF_Lab2b;


namespace WPF_Lab2b {
    
    
    /// <summary>
    /// Image
    /// </summary>
    public partial class Image : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 55 "..\..\Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image myImage;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image myImage2;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Menu AOptions;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\Image.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ErrorText;
        
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
            System.Uri resourceLocater = new System.Uri("/WPF_Lab2b;component/image.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Image.xaml"
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
            
            #line 7 "..\..\Image.xaml"
            ((WPF_Lab2b.Image)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 8 "..\..\Image.xaml"
            ((WPF_Lab2b.Image)(target)).MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.RightMouse_Down);
            
            #line default
            #line hidden
            
            #line 8 "..\..\Image.xaml"
            ((WPF_Lab2b.Image)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.LeftMouse_Down);
            
            #line default
            #line hidden
            return;
            case 2:
            this.myImage = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.myImage2 = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.AOptions = ((System.Windows.Controls.Menu)(target));
            return;
            case 5:
            
            #line 70 "..\..\Image.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PlayAndPause);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 71 "..\..\Image.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Stop);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ErrorText = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
