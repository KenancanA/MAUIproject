using CommunityToolkit.Maui;
using Hotell567.Data;
using Hotell567.Logic;
using Hotell567.MVVM;

namespace Hotell567;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        AppManager.isInitialized = true;


        builder.Services.AddTransient<RoomsPage>();
        builder.Services.AddTransient<RoomsViewModel>();

        builder.Services.AddTransient<RoomDetailPage>();
        builder.Services.AddTransient<RoomDetailViewModel>();

        return builder.Build();
    }
}
