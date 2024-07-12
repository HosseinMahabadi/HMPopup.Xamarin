using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using HMExtension.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using System.Drawing;

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

        private string _headerFontFamily = "Arial";
        public string HeaderFontFamily
        {
            get => _headerFontFamily;
            set => SetProperty(ref _headerFontFamily, value);
        }

        private double _headerFontSize = Application.Current.GetNamedSize(NamedSizes.Medium);
        public double HeaderFontSize
        {
            get => _headerFontSize;
            set => SetProperty(ref _headerFontSize, value);
        }

        private string _messageFontFamily = null;
        public string MessageFontFamily
        {
            get => _messageFontFamily;
            set => SetProperty(ref _messageFontFamily, value);
        }
        private double _messageFontSize = Application.Current.GetNamedSize(NamedSizes.Body);   
        public double MessageFontSize
        {
            get => _messageFontSize;
            set => SetProperty(ref _messageFontSize, value);
        }

        private string _footerFontFamily = null;
        public string FooterFontFamily
        {
            get => _footerFontFamily;
            set => SetProperty(ref _footerFontFamily, value);
        }

        private double _footerFontSize = Application.Current.GetNamedSize(NamedSizes.Body);
        public double FooterFontSize
        {
            get => _footerFontSize;
            set => SetProperty(ref _footerFontSize, value);
        }

        private string _listFontFamily = null;
        public string ListFontFamily
        {
            get => _listFontFamily;
            set => SetProperty(ref _listFontFamily, value);
        }

        private double _listFontSize = Application.Current.GetNamedSize(NamedSizes.Medium);
        public double ListFontSize
        {
            get => _listFontSize;
            set => SetProperty(ref _listFontSize, value);
        }

        private ObservableCollection<SelectableItem<T>> _items = [];
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
                    _selectedItem?.UnSelect();
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
