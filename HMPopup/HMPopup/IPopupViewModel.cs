using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using HMExtension.Xamarin.Mvvm;

namespace HMPopup
{
    internal interface IPopupViewModel
    {
        ListView ListView { get; set; }
        FlowDirection MessageFlowDirection { get; set; }
        string PopupTitle { get; set; }
        string Message { get; set; }
        bool Button1Visibility { get; set; }
        bool Button2Visibility { get; set; }
        string Button1Title { get; set; }
        string Button2Title { get; set; }
        Command Button1Command { get; }
        Command Button2Command { get; }
        void GoToSelectedItem();
    }
}
