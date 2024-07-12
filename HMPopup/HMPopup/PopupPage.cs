using System;
using System.Collections.Generic;
using System.Text;
using HMExtension.Maui;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.ApplicationModel;
using System.Diagnostics;
using Microsoft.Maui.Platform;

namespace HMPopup;

internal class PopupPage : ContentPage
{
    public PopupPage(IPopupViewModel viewModel, bool isSelection = false)
    {
        DataContext = viewModel;
        IsSelection = isSelection;
        InitializeComponent();
        viewModel.GoToSelectedItem();
    }

    private bool IsSelection { get; set; } = false;
    private IPopupViewModel DataContext { get; set; } = null;
    public Grid MainGrid { get; set; } = null;
    private ListView ListView { get; set; } = null;

    private void InitializeComponent()
    {
        BackgroundColor = Colors.Transparent;
        ListView = CreateMessageListView();
        DataContext.ListView = ListView;
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
        Grid grid = new()
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
            Padding = 0,
            BackgroundColor = Globals.PageTransparentColor,
        };
        grid.Children.Add(CreateMainFrame());
        
        return grid;
    }

    private Frame CreateMainFrame()
    {
        Frame frame = new()
        {
            CornerRadius = 15,
            Padding = 0,
            Margin = 0,
        };

        frame.SetValue(Grid.RowProperty, 1);
        frame.SetValue(Grid.ColumnProperty, 1);
        frame.SetAppThemeColor(BackgroundColorProperty, Globals.MainLightColor, Globals.MainDarkColor);
        frame.SetAppThemeColor(Microsoft.Maui.Controls.Frame.BorderColorProperty, Globals.DarkPrimaryColor, Globals.LightPrimaryColor);

        frame.Content = CreateMainMessageGrid();

        return frame;
    }

    private Grid CreateMainMessageGrid()
    {
        Grid grid = new()
        {
            RowDefinitions =
            {
                new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
                new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
                new RowDefinition(){Height = new GridLength(1, GridUnitType.Star)},
                new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
                new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
            },
            Padding = 0,
        };
      
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
        Label label = new()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = DataContext.HeaderFontSize,
            Text = DataContext.PopupTitle,
            Margin = new Thickness(5, 10),
        };

        if(DataContext.HeaderFontFamily != null)
        {
            label.FontFamily = DataContext.HeaderFontFamily;
        }

        label.SetAppThemeColor(Label.TextColorProperty, Globals.MainDarkColorControls, Globals.MainLightColorControls);
        
        return label;
    }

    private static BoxView CreateSeperator()
    {
        BoxView boxView = new()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.End,
            HeightRequest = 0.5,
        };

        boxView.SetAppThemeColor(BackgroundColorProperty, Globals.DarkPrimaryColor, Globals.LightPrimaryColor);
        
        return boxView;
    }

    private Grid CreateMessageHeaderGrid()
    {
        Grid grid = new()
        {
            RowDefinitions =
            {
                new RowDefinition(){Height = new GridLength(1, GridUnitType.Star)},
                new RowDefinition(){Height = new GridLength(1, GridUnitType.Auto)},
            },

            IsClippedToBounds = true,
            VerticalOptions= LayoutOptions.Fill,
        };
        
        if(IsSelection)
        {
            grid.RowDefinitions = new()
            {
                new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) },
                new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) },
            };
        }
        grid.SetValue(Grid.RowProperty, 2);

        grid.Children.Add(CreateMessageGrid());
        if (IsSelection)
        {
            grid.Children.Add(ListView);
        }

        return grid;
    }

    private Grid CreateMessageGrid()
    {
        Grid grid = new()
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(){Width = new GridLength(50)},
                new ColumnDefinition(){Width = new GridLength(1, GridUnitType.Star)},
            },
            IsClippedToBounds= true,
        };

        grid.Children.Add(CreateMessageIcon());
        grid.Children.Add(CreateMessageScrollView());

        return grid;
    }

    private static Image CreateMessageIcon()
    {
        Image image = new()
        {
            Source = ImageSource.FromResource("HMPopup.Resources.Message.png"),
            HeightRequest = 40,
            Margin = new Thickness(5, 0, 5, 10),
            VerticalOptions = LayoutOptions.Start,
        };

        TintImageEffect effect = new()
        {
            TintColor = Application.Current.RequestedTheme == AppTheme.Light ? Globals.DarkPrimaryColor : Globals.LightPrimaryColor
        };
        image.Effects.Add(effect);

        return image;
    }

    private ScrollView CreateMessageScrollView()
    {
        ScrollView scrollView = new()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            IsClippedToBounds = true,
            Content = CreateMessageLabel(),
        };
        scrollView.SetValue(Grid.ColumnProperty, 1);

        return scrollView;
    }

    private Label CreateMessageLabel()
    {
        Label label = new()
        {
            MaxLines = 200,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(5, 10, 5, 10),
            FontSize = DataContext.MessageFontSize,
            Text = DataContext.Message,
        };

        if (DataContext.MessageFontFamily != null)
        {
            label.FontFamily = DataContext.MessageFontFamily;
        }

        label.SetAppThemeColor(Label.TextColorProperty, Globals.MainDarkColorControls, Globals.MainLightColorControls);

        return label;
    }

    private ListView CreateMessageListView()
    {
        ListView listView = new()
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
        Label label = new()
        {
            FontSize = DataContext.ListFontSize,
            HorizontalOptions = LayoutOptions.Fill,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Fill,
            VerticalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(10),
        };

        if (DataContext.ListFontFamily != null)
        {
            label.FontFamily = DataContext.ListFontFamily;
        }

        label.SetBinding(Label.TextProperty, new Binding() { Path = "Title" });
        label.SetBinding(Label.TextColorProperty, new Binding() { Path = "TextColor" });
        label.SetBinding(ScaleProperty, new Binding() { Path = "Scale" });

        return label;
    }

    private HorizontalStackLayout CreateFooterStackLayout()
    {
        HorizontalStackLayout stackLayout = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            Spacing = 0,
        };

        stackLayout.SetValue(Grid.RowProperty, 4);

        stackLayout.Children.Add(CreateButton1());
        stackLayout.Children.Add(CreateButton2());

        return stackLayout;
    }

    private Button CreateButton1()
    {
        Button button = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(10, 0),
            FontSize = DataContext.FooterFontSize,
            Padding = new Thickness(10),
            BackgroundColor = Colors.Transparent,
        };

        if (DataContext.FooterFontFamily != null)
        {
            button.FontFamily = DataContext.FooterFontFamily;
        }

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
        Button button = new()
        {
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(5, 0),
            FontSize = DataContext.FooterFontSize,
            Padding = new Thickness(10),
            BackgroundColor = Colors.Transparent,
        };

        if (DataContext.FooterFontFamily != null)
        {
            button.FontFamily = DataContext.FooterFontFamily;
        }

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
