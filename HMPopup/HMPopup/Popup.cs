using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HMExtension.Xamarin.Controls;

namespace HMPopup
{
    public class Popup : IPopup
    {
        public string OkTitle { get; set; } = "Ok";
        public string CancelTitle { get; set; } = "Cancel";
        public string SelectTitle { get; set; } = "Select";
        public string YesTitle { get; set; } = "Yes";
        public string NoTitle { get; set; } = "No";

        /// <summary>
        /// Main color for light theme
        /// </summary>
        public Color LightThemeColor { get; set; } = Color.WhiteSmoke;

        /// <summary>
        /// Main color for dark theme
        /// </summary>
        public Color DarkThemeColor { get; set; } = Color.FromHex("#232323");

        /// <summary>
        /// Selected text color for light theme
        /// </summary>
        public Color SelectedLightColor { get; set; } = Color.DarkOrange;

        /// <summary>
        /// Selected text color for dark theme
        /// </summary>
        public Color SelectedDarkColor { get; set; } = Color.FromHex("#fee440");

        public FlowDirection FlowDirection { get; set; } = FlowDirection.LeftToRight;

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
                MessageFlowDirection = FlowDirection,
            };

            var TargetPage = new PopupPage(ViewModel);
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
                MessageFlowDirection = FlowDirection,
            };

            var TargetPage = new PopupPage(ViewModel);
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
                MessageFlowDirection = FlowDirection,
            };

            var TargetPage = new PopupPage(ViewModel);
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
                MessageFlowDirection = FlowDirection,
            };

            var TargetPage = new PopupPage(ViewModel);
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
                MessageFlowDirection = FlowDirection,
            };

            var TargetPage = new PopupPage(ViewModel);
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
                MessageFlowDirection = FlowDirection,
            };

            var temp = GetSelectableItems(Items);
            ViewModel.Items = temp;

            var TargetPage = new PopupPage(ViewModel);
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
                MessageFlowDirection = FlowDirection,
                DefaultValue = SelectedItem,
            };

            var temp = GetSelectableItems(Items);
            ViewModel.Items = temp;
            ViewModel.SelectedItem = temp.FirstOrDefault(i => i.Title.Equals(SelectedItem));

            var TargetPage = new PopupPage(ViewModel);
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
                MessageFlowDirection = FlowDirection,
            };

            var temp = GetSelectableItems(Items);
            ViewModel.Items = temp;

            var TargetPage = new PopupPage(ViewModel);
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
                MessageFlowDirection = FlowDirection,
                DefaultValue = SelectedItem,
            };

            var temp = GetSelectableItems(Items);
            ViewModel.Items = temp;
            ViewModel.SelectedItem = temp.FirstOrDefault(i => i.Title.Equals(SelectedItem));

            var TargetPage = new PopupPage(ViewModel);
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
}
