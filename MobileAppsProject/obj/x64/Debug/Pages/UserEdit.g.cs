﻿#pragma checksum "C:\Users\lelis\documents\visual studio 2015\Projects\MobileAppsProject\MobileAppsProject\Pages\UserEdit.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C035DC955C7F8C40B9C5FD1BBA7AA16F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MobileAppsProject.Pages
{
    partial class UserEdit : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.txtEnergy = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 2:
                {
                    this.txtFat = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3:
                {
                    this.txtSaturates = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4:
                {
                    this.txtSugar = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.txtSalt = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    this.SaveBtn = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 55 "..\..\..\Pages\UserEdit.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.SaveBtn).Click += this.SaveBtn_Click;
                    #line default
                }
                break;
            case 7:
                {
                    this.timeWakeUpTime = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 8:
                {
                    this.timeLunchUpTime = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 9:
                {
                    this.timeDinnerUpTime = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 10:
                {
                    this.timeBedUpTime = (global::Windows.UI.Xaml.Controls.TimePicker)(target);
                }
                break;
            case 11:
                {
                    this.txtName = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 12:
                {
                    this.txtAge = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 13:
                {
                    this.txtHeight = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 14:
                {
                    this.txtWeight = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

