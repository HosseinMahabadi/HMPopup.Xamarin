using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using HMExtension.Xamarin;

namespace HMPopup
{
    internal class PopupPage : ContentPage
    {
        public PopupPage(IPopupViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            viewModel.GoToSelectedItem();
        }

        private IPopupViewModel DataContext { get; set; } = null;
        public Grid MainGrid { get; set; } = null;
        private ListView listView { get; set; } = null;
        private void InitializeComponent()
        {
            BackgroundColor = Color.Transparent;
            listView = CreateMessageListView();
            DataContext.ListView = listView;
            MainGrid = CreateMainGrid();

            Appearing += async (_, __) =>
            {
                _ = await MainGrid.FadeTo(1);
            };

            Disappearing += async (_, __) =>
            {
                _ = await MainGrid.FadeTo(0, 100);
            };

            Content = MainGrid;
        }

        private Grid CreateMainGrid()
        {
            Grid grid = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition() { Height = new GridLength(0.75, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(1.5, GridUnitType.Star) },
                    new RowDefinition() { Height = new GridLength(0.75, GridUnitType.Star) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(0.25, GridUnitType.Star) },
                    new ColumnDefinition() { Width = new GridLength(3.5, GridUnitType.Star) },
                    new ColumnDefinition() { Width = new GridLength(0.25, GridUnitType.Star) },
                },
                Opacity = 0,
                BackgroundColor = Globals.PageTransparentColor,
            };

            grid.SetBinding(FlowDirectionProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.MessageFlowDirection),
            });

            grid.Children.Add(CreateMainFrame());
            grid.Children.Add(CreateMainMessageGrid());

            return grid;
        }

        private Frame CreateMainFrame()
        {
            Frame frame = new Frame()
            {
                CornerRadius = 15,
            };

            frame.SetValue(Grid.RowProperty, 1);
            frame.SetValue(Grid.ColumnProperty, 1);
            frame.SetAppThemeColor(BackgroundColorProperty, Globals.MainLightColor, Globals.MainDarkColor);
            frame.SetAppThemeColor(Frame.BorderColorProperty, Globals.DarkPrimaryColor, Globals.LightPrimaryColor);

            return frame;
        }

        private Grid CreateMainMessageGrid()
        {
            Grid grid = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
                    new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
                    new RowDefinition(){Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
                    new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
                }
            };

            grid.SetValue(Grid.RowProperty, 1);
            grid.SetValue(Grid.ColumnProperty, 1);

            grid.Children.Add(CreatePopupTitleLabel());
            var seperator1 = CreateSeperator();
            seperator1.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(seperator1);
            grid.Children.Add(CreateMessageHeaderGrid());
            var seperator2 = CreateSeperator();
            seperator1.SetValue(Grid.RowProperty, 3);
            grid.Children.Add(seperator2);
            grid.Children.Add(CreateFooterStackLayout());

            return grid;
        }

        private Label CreatePopupTitleLabel()
        {
            Label label = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontFamily = "samimBold",
                Margin = new Thickness(5, 10),
            };

            label.SetBinding(Label.TextProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.PopupTitle),
            });

            label.SetAppThemeColor(Label.TextColorProperty, Globals.MainDarkColorControls, Globals.MainLightColorControls);
            
            return label;
        }

        private BoxView CreateSeperator()
        {
            BoxView boxView = new BoxView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 0.5,
            };

            boxView.SetAppThemeColor(BackgroundColorProperty, Globals.DarkPrimaryColor, Globals.LightPrimaryColor);

            return boxView;
        }

        private Grid CreateMessageHeaderGrid()
        {
            Grid grid = new Grid()
            {
                RowDefinitions =
                {
                    new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
                    new RowDefinition(){Height = new GridLength(1, GridUnitType.Star)},
                }
            };
            
            grid.SetValue(Grid.RowProperty, 2);

            grid.Children.Add(CreateMessageGrid());
            grid.Children.Add(listView);

            return grid;
        }

        private Grid CreateMessageGrid()
        {
            Grid grid = new Grid()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(){Width = new GridLength(1, GridUnitType.Auto)},
                    new ColumnDefinition(){Width = new GridLength(1, GridUnitType.Star)},
                },
            };

            grid.Children.Add(CreateMessageIcon());
            grid.Children.Add(CreateMessageLabel());

            return grid;
        }

        private Image CreateMessageIcon()
        {
            Image image = new Image
            {
                Source = ImageSource.FromResource("HMPopup.Resources.Message.png"),
                HeightRequest = 40,
                Margin = new Thickness(5, 0, 5, 10),
                VerticalOptions = LayoutOptions.Start,
            };

            TintImageEffect effect = new TintImageEffect
            {
                TintColor = Application.Current.RequestedTheme == OSAppTheme.Light ? Globals.DarkPrimaryColor : Globals.LightPrimaryColor
            };
            image.Effects.Add(effect);

            return image;
        }

        private Label CreateMessageLabel()
        {
            Label label = new Label()
            {
                MaxLines = 20,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Margin = new Thickness(5, 10, 5, 10),
                FontSize = 15,
                FontFamily = "samim",
            };

            label.SetBinding(Label.TextProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.Message),
            });

            label.SetAppThemeColor(Label.TextColorProperty, Globals.MainDarkColorControls, Globals.MainLightColorControls);
            label.SetValue(Grid.ColumnProperty, 1);

            return label;
        }

        private ListView CreateMessageListView()
        {
            ListView listView = new ListView()
            {
                SeparatorVisibility = SeparatorVisibility.None,
                SelectionMode = ListViewSelectionMode.Single,
                Margin = new Thickness(0, 15, 0, 10),
            };

            listView.SetBinding(ListView.ItemsSourceProperty, new Binding()
            {
                Source = DataContext,
                Path = "Items",
            });

            listView.SetBinding(ListView.SelectedItemProperty, new Binding()
            {
                Source = DataContext,
                Path = "SelectedItem",
                Mode = BindingMode.TwoWay,
            });

            listView.ItemTemplate = new DataTemplate(() =>
            {
                return new ViewCell()
                {
                    View = CreateListViewItemTemplate(),
                };
            });

            listView.SetValue(Grid.RowProperty, 1);

            return listView;
        }

        private Label CreateListViewItemTemplate()
        {
            Label label = new Label()
            {
                FontFamily = "samim",
                FontSize = 17,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(10),
            };

            label.SetBinding(Label.TextProperty, new Binding() { Path = "Title" });
            label.SetBinding(Label.TextColorProperty, new Binding() { Path = "TextColor" });
            label.SetBinding(Label.ScaleProperty, new Binding() { Path = "Scale" });

            return label;
        }

        private StackLayout CreateFooterStackLayout()
        {
            StackLayout stackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Spacing = 0,
            };

            stackLayout.SetValue(Grid.RowProperty, 4);

            stackLayout.Children.Add(CreateButton1());
            stackLayout.Children.Add(CreateButton2());

            return stackLayout;
        }

        private Button CreateButton1()
        {
            Button button = new Button()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(5, 0),
                FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Button)),
                FontFamily = "samim",
                Padding = 0,
                BackgroundColor = Color.Transparent,
            };

            button.SetBinding(Button.TextProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.Button1Title),
            });

            button.SetBinding(IsVisibleProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.Button1Visibility),
            });

            button.SetBinding(Button.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.Button1Command),
            });

            button.SetAppThemeColor(Button.TextColorProperty, Globals.DarkPrimaryColor, Globals.LightPrimaryColor);

            return button;
        }

        private Button CreateButton2()
        {
            Button button = new Button()
            {
                Margin = new Thickness(5, 0),
                FontSize = Device.GetNamedSize(NamedSize.Body, typeof(Button)),
                FontFamily = "samim",
                Padding = 0,
                BackgroundColor = Color.Transparent,
            };

            button.SetBinding(Button.TextProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.Button2Title),
            });

            button.SetBinding(IsVisibleProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.Button2Visibility),
            });

            button.SetBinding(Button.CommandProperty, new Binding()
            {
                Source = DataContext,
                Path = nameof(DataContext.Button2Command),
            });

            button.SetAppThemeColor(Button.TextColorProperty, Globals.DarkPrimaryColor, Globals.LightPrimaryColor);

            return button;
        }
    }
}
