using HMPopup;

namespace HMPopupSample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private readonly Popup englishPopup = new()
    {
        OkTitle = "Ok",
        CancelTitle = "Cancel",
        NoTitle = "No",
        YesTitle = "Yes",
        SelectTitle = "Select",
        FlowDirection = FlowDirection.LeftToRight,
    };

    private readonly Popup persianPopup = new()
    {
        OkTitle = "تایید",
        CancelTitle = "انصراف",
        NoTitle = "خیر",
        YesTitle = "بله",
        SelectTitle = "انتخاب",
        FlowDirection = FlowDirection.RightToLeft,
        HeaderFontFamily = "samimBold",
        MessageFontFamily = "samim",
        FooterFontFamily = "samim",
        ListFontFamily = "samim"
    };

    private string englishSelectedItem = "sarah";

    private string persianSelectedItem = "سارا";

    private async void EnglishMessageButton_Clicked(object sender, EventArgs e)
    {
        await englishPopup.ShowMessageAsync("Test Message", messageEntry.Text);
    }

    private async void PersianMessageButton_Clicked(object sender, EventArgs e)
    {
        await persianPopup.ShowMessageAsync("پیام آزمایشی", messageEntry.Text);
    }

    private async void EnglishQuestionButton_Clicked(object sender, EventArgs e)
    {
        var answer = await englishPopup.ShowQuestionAsync("Test Question", "Are you sure?");
        var str = answer ? "Yes" : "No";
        questionLabel.Text = $"Answer => {str}";
    }

    private async void PersianQuestionButton_Clicked(object sender, EventArgs e)
    {
        var answer = await persianPopup.ShowQuestionAsync("سوال آزمایشی", "آیا اطمینان دارید؟");
        var str = answer ? "بله" : "خیر";
        questionLabel.Text = $"پاسخ => { str }";
    }

    private async void EnglishSelectionButton_Clicked(object sender, EventArgs e)
    {
        var list = new List<string>()
            {
                "james",
                "adam",
                "alison",
                "tommy",
                "amanda",
                "paul",
                "jennifer",
                "peter",
                "sarah",
                "mandy"
            };

        var answer = await englishPopup.ShowSelectionAsync("Test selection", "Please select:", list, englishSelectedItem);
        if (!string.IsNullOrEmpty(answer))
        {
            englishSelectedItem = answer;
            selectionLabel.Text = $"Selected item => { answer }";
        }
    }

    private async void PersianSelectionButton_Clicked(object sender, EventArgs e)
    {
        var list = new List<string>()
            {
                "جواد",
                "احمد",
                "آرزو",
                "رضا",
                "لیلا",
                "پدرام",
                "زهرا",
                "محمد",
                "سارا",
                "فاطمه"
            };

        var answer = await persianPopup.ShowSelectionAsync("انتخاب آزمایشی", "لطفا انتخاب کنید:", list, persianSelectedItem);
        if (!string.IsNullOrEmpty(answer))
        {
            persianSelectedItem = answer;
            selectionLabel.Text = $"مورد انتخاب شده => { answer }";
        }
    }
}
