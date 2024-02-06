using Hotell567.Logic;

namespace Hotell567.MVVM;

public partial class MainPage : ContentPage
{
    private string welcomeLabelOriginalText;
    private string nameLabelOriginalText;

    public MainPage()
    {
        InitializeComponent();
        welcomeLabelOriginalText = WelcomeLabel.Text;
        nameLabelOriginalText = NameLabel.Text;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        TypewriterEffect();
    }


    private void SeeAvailableListings(object sender, EventArgs e)
    {
        if (AppManager.currentUser == null)
        {
            Shell.Current.GoToAsync("//LoginPage");
        }
        else
        {
            Shell.Current.GoToAsync("//RoomsPage");
        }
    }

    private async void TypewriterEffect()
    {
        string welcomeText = welcomeLabelOriginalText;
        string nameText = nameLabelOriginalText;
        WelcomeLabel.Text = "";
        NameLabel.Text = "";
        AvailableListingsBtn.Opacity = 0;

        await Task.Delay(TimeSpan.FromSeconds(0.5));

        for (int i = 0; i < welcomeText.Length; i++)
        {
            WelcomeLabel.Text += welcomeText.ElementAt(i);
            await Task.Delay(TimeSpan.FromMilliseconds(30));
        }

        for (int i = 0; i < nameText.Length; i++)
        {
            NameLabel.Text += nameText.ElementAt(i);
            await Task.Delay(TimeSpan.FromMilliseconds(30));
        }

        var fadeInAnimation = new Animation();
        fadeInAnimation.Add(0, 1, new Animation(v => AvailableListingsBtn.Opacity = v, 0, 1));
        fadeInAnimation.Commit(AvailableListingsBtn, "FadeIn", 16, 500);
    }
}

