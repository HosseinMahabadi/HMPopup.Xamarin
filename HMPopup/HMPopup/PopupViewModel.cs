using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HMExtension.Xamarin.Mvvm;
using HMExtension.Xamarin.Controls;

namespace HMPopup
{
    internal class PopupViewModel<T> : ViewModelBase, IPopupViewModel
    {
        #region Properties

        public T DefaultValue { get; set; }
        public ListView ListView { get; set; } = new ListView();

        public TaskCompletionSource<T> TaskCompletion { get; set; } = null;

        private string _popupTitle = "Title";
        public string PopupTitle
        {
            get => _popupTitle;
            set => SetProperty(ref _popupTitle, value);
        }

        private string _message = null;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private FlowDirection _messageFlowDirection = FlowDirection.LeftToRight;
        public FlowDirection MessageFlowDirection
        {
            get => _messageFlowDirection;
            set => SetProperty(ref _messageFlowDirection, value);
        }

        private ObservableCollection<SelectableItem<T>> _items = new ObservableCollection<SelectableItem<T>>();
        public ObservableCollection<SelectableItem<T>> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private SelectableItem<T> _selectedItem = null;
        public SelectableItem<T> SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    if (_selectedItem != null)
                    {
                        _selectedItem.UnSelect();
                    }
                    _selectedItem = value;
                    OnPropertyChanged();
                    value.Select();
                    GoToSelectedItem();
                }
            }
        }

        private bool _button1Visibility = true;
        public bool Button1Visibility
        {
            get => _button1Visibility;
            set => SetProperty(ref _button1Visibility, value);
        }

        private bool _button2Visibility = true;
        public bool Button2Visibility
        {
            get => _button2Visibility;
            set => SetProperty(ref _button2Visibility, value);
        }

        private string _button1Title = null;
        public string Button1Title
        {
            get => _button1Title;
            set => SetProperty(ref _button1Title, value);
        }

        private string _button2Title = null;
        public string Button2Title
        {
            get => _button2Title;
            set => SetProperty(ref _button2Title, value);
        }

        #endregion

        #region Methods

        private async void OnButtonPressed(object Answer)
        {
            try
            {
                _ = await Application.Current.MainPage.Navigation.PopModalAsync(true);
                try
                {
                    TaskCompletion?.SetResult((T)Answer);
                }
                catch
                {
                    TaskCompletion?.SetResult(DefaultValue);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GoToSelectedItem()
        {
            ListView.ScrollTo(SelectedItem, ScrollToPosition.Center, true);
        }

        #endregion 

        private Command _button1Command = null;
        public Command Button1Command
        {
            get
            {
                if (_button1Command == null)
                {
                    _button1Command = new Command(() =>
                    {
                        if (Items.Count > 0)
                        {
                            OnButtonPressed(SelectedItem.Title);
                        }
                        else
                        {
                            OnButtonPressed(true);
                        }
                    });
                }
                return _button1Command;
            }
        }

        private Command _button2Command = null;
        public Command Button2Command
        {
            get
            {
                if (_button2Command == null)
                {
                    _button2Command = new Command(() =>
                    {
                        if (Items.Count > 0)
                        {
                            OnButtonPressed(null);
                        }
                        else
                        {
                            OnButtonPressed(false);
                        }
                    });
                }
                return _button2Command;
            }
        }
    }
}
