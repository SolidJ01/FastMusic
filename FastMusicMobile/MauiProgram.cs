using FastMusicMobile.Services;
using FastMusicMobile.View;
using FastMusicMobile.ViewModel;
using Microsoft.Extensions.Logging;

namespace FastMusicMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FASolid");
                });

            builder.Services.AddSingleton<AudioMasterService>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<AlbumsPageViewModel>();
            builder.Services.AddSingleton<AlbumsPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
