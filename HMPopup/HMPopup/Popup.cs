using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMExtension.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Drawing;
using Microsoft.Maui.Animations;

namespace HMPopup;

public class Popup : IPopup
{
    #region Properties

    public string OkTitle { get; set; } = "Ok";
    public string CancelTitle { get; set; } = "Cancel";
    public string SelectTitle { get; set; } = "Select";
    public string YesTitle { get; set; } = "Yes";
    public string NoTitle { get; set; } = "No";
    public string HeaderFontFamily { get; set; } = null;
    public double HeaderFontSize { get; set; } = Application.Current.GetNamedSize(NamedSizes.Medium);
    public string MessageFontFamily { get; set; } = null;
    public double MessageFontSize { get; set; } = Application.Current.GetNamedSize(NamedSizes.Body);
    public string FooterFontFamily { get; set; } = null;
    public double FooterFontSize { get; set; } = Application.Current.GetNamedSize(NamedSizes.Body);
    public string ListFontFamily { get; set; } = null;  
    public double ListFontSize { get; set; } = Application.Current.GetNamedSize(NamedSizes.Medium);

    /// <summary>
    /// Main color for light theme
    /// </summary>
    public Microsoft.Maui.Graphics.Color LightThemeColor { get; set; } = Colors.WhiteSmoke;

    /// <summary>
    /// Main color for dark theme
    /// </summary>
    public Microsoft.Maui.Graphics.Color DarkThemeColor { get; set; } = Microsoft.Maui.Graphics.Color.FromArgb("#232323");

    /// <summary>
    /// Selected text color for light theme
    /// </summary>
    public Microsoft.Maui.Graphics.Color SelectedLightColor { get; set; } = Colors.DarkOrange;

    /// <summary>
    /// Selected text color for dark theme
    /// </summary>
    public Microsoft.Maui.Graphics.Color SelectedDarkColor { get; set; } = Microsoft.Maui.Graphics.Color.FromArgb("#fee440");

    public FlowDirection FlowDirection { get; set; } = FlowDirection.LeftToRight;

    #endregion

    public async void ShowMessage(string Title, string Message)
    {
        var TaskCompletionBool = new TaskCompletionSource<bool>();

        var ViewModel = new PopupViewModel<bool>()
        {
            PopupTitle = Title,
            Message = Message,
            Button1Title = OkTitle,
            Button2Visibility = false,
            TaskCompletion = TaskCompletionBool,
            HeaderFontFamily = HeaderFontFamily,
            HeaderFontSize = HeaderFontSize,
            FooterFontFamily = FooterFontFamily,
            FooterFontSize = FooterFontSize,
            MessageFontFamily = MessageFontFamily,
            MessageFontSize = MessageFontSize,
        };

        PopupPage TargetPage = new(ViewModel)
        {
            FlowDirection = FlowDirection
        };
        await Application.Current.MainPage.Navigation.PushModalAsync(TargetPage, false);
        await TaskCompletionBool.Task;
    }

    public Task ShowMessageAsync(string Title, string Message)
    {
        var TaskCompletionBool = new TaskCompletionSource<bool>();

        var ViewModel = new PopupViewModel<bool>()
        {
            PopupTitle = Title,
            Message = Message,
            Button1Title = OkTitle,
            Button2Visibility = false,
            TaskCompletion = TaskCompletionBool,
            HeaderFontFamily = HeaderFontFamily,
            HeaderFontSize = HeaderFontSize,
            FooterFontFamily = FooterFontFamily,
            FooterFontSize = FooterFontSize,
            MessageFontFamily = MessageFontFamily,
            MessageFontSize = MessageFontSize,
        };

        var TargetPage = new PopupPage(ViewModel);
        TargetPage.FlowDirection = FlowDirection;    
        Application.Current.MainPage.Navigation.PushModalAsync(TargetPage, false);
        return TaskCompletionBool.Task;
    }

    public Task ShowMessageAsync(string Title, string Message, string OkTitle)
    {
        var TaskCompletionBool = new TaskCompletionSource<bool>();

        var ViewModel = new PopupViewModel<bool>()
        {
            PopupTitle = Title,
            Message = Message,
            Button1Title = OkTitle,
            Button2Visibility = false,
            TaskCompletion = TaskCompletionBool,
            HeaderFontFamily = HeaderFontFamily,
            HeaderFontSize = HeaderFontSize,
            FooterFontFamily = FooterFontFamily,
            FooterFontSize = FooterFontSize,
            MessageFontFamily = MessageFontFamily,
            MessageFontSize = MessageFontSize,
        };

        var TargetPage = new PopupPage(ViewModel)
        {
            FlowDirection = FlowDirection
        };
        Application.Current.MainPage.Navigation.PushModalAsync(TargetPage, false);
        return TaskCompletionBool.Task;
    }

    //=-=-=-=-=-=-=-=-=-=-= ShowQuestion =-=-=-=-=-=-=-=-=-=-=

    public Task<bool> ShowQuestionAsync(string Title, string Question)
    {
        var TaskCompletionBool = new TaskCompletionSource<bool>();

        var ViewModel = new PopupViewModel<bool>()
        {
            PopupTitle = Title,
            Message = Question,
            Button1Title = YesTitle,
            Button2Title = NoTitle,
            TaskCompletion = TaskCompletionBool,
            HeaderFontFamily = HeaderFontFamily,
            HeaderFontSize = HeaderFontSize,
            FooterFontFamily = FooterFontFamily,
            FooterFontSize = FooterFontSize,
            MessageFontFamily = MessageFontFamily,
            MessageFontSize = MessageFontSize,
        };

        PopupPage TargetPage = new(ViewModel)
        {
            FlowDirection = FlowDirection
        };
        Application.Current.MainPage.Navigation.PushModalAsync(TargetPage, false);
        return TaskCompletionBool.Task;
    }

    public Task<bool> ShowQuestionAsync(string Title, string Question, string YesTitle, string NoTitle)
    {
        var TaskCompletionBool = new TaskCompletionSource<bool>();
        var ViewModel = new PopupViewModel<bool>()
        {
            PopupTitle = Title,
            Message = Question,
            Button1Title = YesTitle,
            Button2Title = NoTitle,
            TaskCompletion = TaskCompletionBool,
            HeaderFontFamily = HeaderFontFamily,
            HeaderFontSize = HeaderFontSize,
            FooterFontFamily = FooterFontFamily,
            FooterFontSize = FooterFontSize,
            MessageFontFamily = MessageFontFamily,
            MessageFontSize = MessageFontSize,
        };

        var TargetPage = new PopupPage(ViewModel)
        {
            FlowDirection = FlowDirection
        };
        Application.Current.MainPage.Navigation.PushModalAsync(TargetPage, false);
        return TaskCompletionBool.Task;
    }

    //-=-=-=-=-=-=-=-=-= ShowSelection =-=-=-=-=-=-=-=-=

    public Task<T> ShowSelectionAsync<T>(string Title, string Message, IList<T> Items)
    {
        var TaskCompletion = new TaskCompletionSource<T>();

        var ViewModel = new PopupViewModel<T>()
        {
            PopupTitle = Title,
            Message = Message,
            Button1Title = SelectTitle,
            Button2Title = CancelTitle,
            TaskCompletion = TaskCompletion,
            HeaderFontFamily = HeaderFontFamily,
            HeaderFontSize = HeaderFontSize,
            FooterFontFamily = FooterFontFamily,
            FooterFontSize = FooterFontSize,
            MessageFontFamily = MessageFontFamily,
            MessageFontSize = MessageFontSize,
        };

        var temp = GetSelectableItems(Items);
        ViewModel.Items = temp;

        var TargetPage = new PopupPage(ViewModel, true)
        {
            FlowDirection = FlowDirection
        };
        Application.Current.MainPage.Navigation.PushModalAsync(TargetPage, false);
        return TaskCompletion.Task;
    }

    public Task<T> ShowSelectionAsync<T>(string Title, string Message, IList<T> Items, T SelectedItem)
    {
        var TaskCompletion = new TaskCompletionSource<T>();

        var ViewModel = new PopupViewModel<T>()
        {
            PopupTitle = Title,
            Message = Message,
            Button1Title = SelectTitle,
            Button2Title = CancelTitle,
            TaskCompletion = TaskCompletion,
            DefaultValue = SelectedItem,
            HeaderFontFamily = HeaderFontFamily,
            HeaderFontSize = HeaderFontSize,
            FooterFontFamily = FooterFontFamily,
            FooterFontSize = FooterFontSize,
            MessageFontFamily = MessageFontFamily,
            MessageFontSize = MessageFontSize,
        };

        var temp = GetSelectableItems(Items);
        ViewModel.Items = temp;
        ViewModel.SelectedItem = temp.FirstOrDefault(i => i.Title.Equals(SelectedItem));

        var TargetPage = new PopupPage(ViewModel, true)
        {
            FlowDirection = FlowDirection
        };
        Application.Current.MainPage.Navigation.PushModalAsync(TargetPage, false);
        return TaskCompletion.Task;
    }

    public Task<T> ShowSelectionAsync<T>(string Title, string Message, IList<T> Items, string SelectTitle, string CancelTitle)
    {
        var TaskCompletion = new TaskCompletionSource<T>();

        var ViewModel = new PopupViewModel<T>()
        {
            PopupTitle = Title,
            Message = Message,
            Button1Title = SelectTitle,
            Button2Title = CancelTitle,
            TaskCompletion = TaskCompletion,
            HeaderFontFamily = HeaderFontFamily,
            HeaderFontSize = HeaderFontSize,
            FooterFontFamily = FooterFontFamily,
            FooterFontSize = FooterFontSize,
            MessageFontFamily = MessageFontFamily,
            MessageFontSize = MessageFontSize,
        };

        var temp = GetSelectableItems(Items);
        ViewModel.Items = temp;

        var TargetPage = new PopupPage(ViewModel, true)
        {
            FlowDirection = FlowDirection
        };
        Application.Current.MainPage.Navigation.PushModalAsync(TargetPage, false);
        return TaskCompletion.Task;
    }

    public Task<T> ShowSelectionAsync<T>(string Title, string Message, IList<T> Items, T SelectedItem, string SelectTitle, string CancelTitle)
    {
        var TaskCompletion = new TaskCompletionSource<T>();

        var ViewModel = new PopupViewModel<T>()
        {
            PopupTitle = Title,
            Message = Message,
            Button1Title = SelectTitle,
            Button2Title = CancelTitle,
            TaskCompletion = TaskCompletion,
            DefaultValue = SelectedItem,
            HeaderFontFamily = HeaderFontFamily,
            HeaderFontSize = HeaderFontSize,
            FooterFontFamily = FooterFontFamily,
            FooterFontSize = FooterFontSize,
            MessageFontFamily = MessageFontFamily,
            MessageFontSize = MessageFontSize,
        };

        var temp = GetSelectableItems(Items);
        ViewModel.Items = temp;
        ViewModel.SelectedItem = temp.FirstOrDefault(i => i.Title.Equals(SelectedItem));

        var TargetPage = new PopupPage(ViewModel, true)
        {
            FlowDirection = FlowDirection
        };
        Application.Current.MainPage.Navigation.PushModalAsync(TargetPage, false);
        return TaskCompletion.Task;
    }

    private ObservableCollection<SelectableItem<T>> GetSelectableItems<T>(IList<T> Items)
    {
        var temp = new ObservableCollection<SelectableItem<T>>();
        foreach (var item in Items)
        {
            temp.Add(new SelectableItem<T>()
            {
                Title = item,
                DarkColor = LightThemeColor,
                LightColor = DarkThemeColor,
                SelectedLightColor = SelectedLightColor,
                SelectedDarkColor = SelectedDarkColor,
            });
        }
        return temp;
    }
}
