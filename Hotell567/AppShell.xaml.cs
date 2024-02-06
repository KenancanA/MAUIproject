using Hotell567.MVVM;

namespace Hotell567;

public partial class AppShell : Shell
{
    private static AppShell appShellSingleton;

    public AppShell()
    {
        InitializeComponent();
        appShellSingleton = this;

        Routing.RegisterRoute(nameof(RoomDetailPage), typeof(RoomDetailPage));
    }
    public static AppShell GetSingleton
    {
        get
        {
            return appShellSingleton;
        }
    }

    public void ShowPagesAfterLogin(int permLevel)
    {
        if (permLevel == 0)
        {
            accountTab.IsVisible = true;
        }
        else if (permLevel == 1)
        {
            adminTab.IsVisible = true;
            accountTab.IsVisible = true;
        }

        loginTab.IsVisible = false;
    }

    public void HidePagesAfterLogout()
    {
        adminTab.IsVisible = false;
        accountTab.IsVisible = false;

        loginTab.IsVisible = true;
    }
}
