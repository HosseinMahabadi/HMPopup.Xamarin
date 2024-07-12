using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using HMExtension.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace HMPopup
{
    internal interface IPopupViewModel
    {
        ListView ListView { get; set; }
        string PopupTitle { get; set; }
        string Message { get; set; }
        string HeaderFontFamily { get; set; }
        double HeaderFontSize { get; set; }
        string MessageFontFamily { get; set; }
        double MessageFontSize { get; set; }    
        string FooterFontFamily { get; set; }
        double FooterFontSize { get; set; }
        string ListFontFamily { get; set; }
        double ListFontSize { get; set; }
        bool Button1Visibility { get; set; }
        bool Button2Visibility { get; set; }
        string Button1Title { get; set; }
        string Button2Title { get; set; }
        Command Button1Command { get; }
        Command Button2Command { get; }
        void GoToSelectedItem();
    }
}
